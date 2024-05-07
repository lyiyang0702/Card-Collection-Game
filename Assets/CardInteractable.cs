using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteractable : Interactable
{

    public CardScriptableObject cardInfo;
    public List<CardScriptableObject> cardInfos = new List<CardScriptableObject>();
    public GameObject cardSprite;
    int rng;
    private void Start()
    {
        cardSprite.SetActive(false);
        rng = Random.Range(1, 4);

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
        cardInfos = ResourceManager.Instance.ReturnRandomCardByTier(1,rng);
    }
}
