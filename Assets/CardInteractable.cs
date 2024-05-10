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
        //cardSprite.SetActive(false);
        rng = Random.Range(4, 8);

    }
    public override void InteractAction()
    {
        base.InteractAction();
        var rngTier = Random.Range(1, 3);
        cardInfos = ResourceManager.Instance.ReturnRandomCardByTier(rngTier, rng);
        PlayerController.Instance.inventory.AddCardToInventory(this);

        Destroy(gameObject);
    }

    public override void OnTargetInteractable()
    {
        base.OnTargetInteractable();
        //if (cardSprite.activeSelf) return;
        //cardSprite.SetActive(true);

    }
}
