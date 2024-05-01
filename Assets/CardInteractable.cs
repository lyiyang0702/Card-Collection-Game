using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteractable : Interactable
{

    public CardScriptableObject cardInfo;

    public override void InteractAction()
    {
        base.InteractAction();
        PlayerController.Instance.inventory.AddCardToInventory(this);
        Destroy(gameObject);
    }
}
