using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardUI : MonoBehaviour,IPointerClickHandler
{
    public Image border;
    public Image background;
    public TextMeshProUGUI atkValueText;
    public TextMeshProUGUI elementalTypeText;
    public TextMeshProUGUI cardNameText;
    public Image cardSprite;
    public Image overlay;
    public Image disableOverlay;
    public AudioSource confirm;
    public AudioSource deny;

    //colors
    [SerializeField] private Color borderGreen;
    [SerializeField] private Color borderBlue;
    [SerializeField] private Color borderPurp;
    [SerializeField] private Color borderGold;
    [SerializeField] private Color backRubber;
    [SerializeField] private Color backSteel;
    [SerializeField] private Color backTitanium;
    [SerializeField] private Color backGold;

    // clickable properties
    public bool isClickable = true;
    CardScriptableObject _cardInfo;
    bool isSelected = false;
    bool isDeslected = true;

    private void Start()
    {
        if(CombatManager.Instance.battleState == BattleState.None)
        {
            isClickable = false;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (!UIManager.Instance.canSelectCards) return;
        if (!_cardInfo) return;
        if(!isClickable) return;

        isSelected = !isSelected;
        isDeslected = !isDeslected;

        if (isSelected)
        {
            if (UIManager.Instance.tempSelectedCards.Count >= 5) return;
            if (!CheckIfCardIsDuplicated(_cardInfo))
            {
                UIManager.Instance.tempSelectedCards.Add(_cardInfo);
                ToggleOtherClickableDuplicateCard(false);
                overlay.gameObject.SetActive(true);
                confirm.Play();
            }


        }
        else if (isDeslected)
        {
            if (CheckIfCardIsDuplicated(_cardInfo))
            {
                UIManager.Instance.tempSelectedCards.Remove(_cardInfo);
                ToggleOtherClickableDuplicateCard(true);
                overlay.gameObject.SetActive(false);
                deny.Play();
            }

        }


    }

    public void UpdateCardUI(CardScriptableObject cardInfo)
    {
        _cardInfo = cardInfo;
        switch (cardInfo.colorTier)
        {
            case ColorTier.Green:
                border.color = borderGreen;
                atkValueText.text = "1";
                break;
            case ColorTier.Blue:
                border.color = borderBlue;
                atkValueText.text = "2";
                break;
            case ColorTier.Purple:
                border.color = borderPurp;
                atkValueText.text = "3";
                break;
            case ColorTier.Golden:
                border.color = borderGold;
                atkValueText.text = "4";
                break;
            default:
                break;
        }
        cardNameText.text = cardInfo.displayName;
        elementalTypeText.text = cardInfo.elementalType.ToString();
        switch (cardInfo.elementalType){
            case ElementalType.Gold:
                background.color = backGold;
                break;
            case ElementalType.Rubber:
                background.color = backRubber;
                break;
            case ElementalType.Steel:
                background.color = backSteel;
                break;
            case ElementalType.Titanium:
                background.color = backTitanium;
                break;
            default:
                break;
        }
        cardSprite.sprite = cardInfo.cardSprite;
    }


    bool CheckIfCardIsDuplicated(CardScriptableObject cardInfo)
    {
        foreach (var card in UIManager.Instance.tempSelectedCards)
        {
            if (card.Equals(cardInfo)) return true;
        }
        return false;
    }

    void ToggleOtherClickableDuplicateCard(bool state)
    {
        var inventoryUI = UIManager.Instance.inventoryUI.GetComponent<InventoryUIController>();

        for(int i = 0; i < inventoryUI.inventoryGrid.transform.childCount; i++)
        {
            var cardUI = inventoryUI.inventoryGrid.transform.GetChild(i).GetComponent<CardUI>();
            if (cardUI.isSelected || !cardUI._cardInfo.Equals(_cardInfo)) continue;
            cardUI.isClickable = state;
            cardUI.disableOverlay.gameObject.SetActive(!state);
            
        }
    }
    
}
