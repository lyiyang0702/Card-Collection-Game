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

    public void OnPointerClick(PointerEventData eventData)
    {
        overlay.gameObject.SetActive(true);

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
            default:
                break;
        }

        elementalTypeText.text = cardInfo.elementalType.ToString();
        cardSprite.sprite = cardInfo.cardSprite;
    }
}
