using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    CardScriptableObject _cardInfo;
    bool isSelected = false;
    bool isDeslected = true;
    public void OnPointerClick(PointerEventData eventData)
    {

        if (!UIManager.Instance.canSelectCards) return;
        if (!_cardInfo) return;
        if (UIManager.Instance.tempSelectedCards.Count >= 5) return;
        isSelected = !isSelected;
        isDeslected = !isDeslected;
        Debug.Log("Selcted: " +isSelected);
        Debug.Log("Deselcted: " + isDeslected);
        if (isSelected)
        {
            //isSelected = false;
            //isDeslected = true;
            if (!UIManager.Instance.tempSelectedCards.Contains(_cardInfo))
            {
                UIManager.Instance.tempSelectedCards.Add(_cardInfo);
                overlay.gameObject.SetActive(true);
            }


        }
        else if (isDeslected)
        {
            //isSelected = true;
            //isDeslected = false;
            if (UIManager.Instance.tempSelectedCards.Contains(_cardInfo))
            {
                UIManager.Instance.tempSelectedCards.Remove(_cardInfo);
                overlay.gameObject.SetActive(false);
            }

        }


    }


// Start is called before the first frame update
void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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


}
