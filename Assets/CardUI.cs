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
    public Image cardSprite;
    public Image overlay;
    public bool isClickable = true;
    CardScriptableObject _cardInfo;
    bool isSelected = false;
    bool isDeslected = true;
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
            }


        }
        else if (isDeslected)
        {
            if (CheckIfCardIsDuplicated(_cardInfo))
            {
                UIManager.Instance.tempSelectedCards.Remove(_cardInfo);
                ToggleOtherClickableDuplicateCard(true);
                overlay.gameObject.SetActive(false);
            }

        }


    }

    public void UpdateCardUI(CardScriptableObject cardInfo)
    {
        _cardInfo = cardInfo;
        switch (cardInfo.colorTier)
        {
            case CardScriptableObject.ColorTier.Green:
                border.color = Color.green;
                atkValueText.text = "1";
                break;
            case CardScriptableObject.ColorTier.Blue:
                border.color = Color.blue;
                atkValueText.text = "2";
                break;
            case CardScriptableObject.ColorTier.Purple:
                border.color = new Color(238, 130, 238, 1);
                atkValueText.text = "3";
                break;
            case CardScriptableObject.ColorTier.Golden:
                border.color = new Color(255, 215, 0, 1);
                atkValueText.text = "4";
                break;
            default:
                break;
        }

        elementalTypeText.text = cardInfo.elementalType.ToString();
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
        }
    }
    
}
