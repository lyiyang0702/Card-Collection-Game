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
}
