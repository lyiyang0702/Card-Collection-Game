using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteractable : Interactable
{

    public CardScriptableObject cardInfo;
    public GameObject cardSprite;

    private void Start()
    {
        cardSprite.SetActive(false);


    }
    public override void InteractAction()
    {
        base.InteractAction();
        PlayerController.Instance.inventory.AddCardToInventory(this);
        Destroy(gameObject);
    }

    public override void OnTargetInteractable()
    {
        base.OnTargetInteractable();
        if (cardSprite.activeSelf) return;
        cardSprite.SetActive(true);
        cardInfo = ResourceManager.Instance.ReturnRandomCard(1)[0];
        //Debug.Log(cardInfo.name);
    }
}
