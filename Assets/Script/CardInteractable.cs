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
        UIManager.Instance.rewardPanelUI.gameObject.SetActive(true);
        UIManager.Instance.rewardPanelUI.PopulateRewardCard(cardInfos);
        Destroy(gameObject);
        //setInvisible();
        //StartCoroutine(getRidOf());
    }

    public override void OnTargetInteractable()
    {
        base.OnTargetInteractable();
        //if (cardSprite.activeSelf) return;
        //cardSprite.SetActive(true);

    }

    public override void OnStopTargetInteractable()
    {
        base.OnStopTargetInteractable();
        
    }
    private void setInvisible(){
        cardSprite.SetActive(false);
    }

    IEnumerator getRidOf(){
        yield return new WaitForSeconds (1.0f);
        Destroy(gameObject);
    }
}
