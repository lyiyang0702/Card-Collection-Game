using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<CardScriptableObject> cards =  new List<CardScriptableObject>();
    public int size = 40;
    //public List<GameObject> cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCardToInventory(CardInteractable cardInteractable)
    {
        if (cards.Count > size) return;
        foreach (CardScriptableObject card in cardInteractable.cardInfos)
        {
            cards.Add(card);
        }
        
    }

    public void RemveCard(CardScriptableObject card)
    {
        cards.Remove(card);
    }

    //public void AddCardToInventory(CardInteractable cardInteractable)
    //{
    //    GameObject cardObj = new GameObject("card");
    //    var card = cardObj.AddComponent<CardDamageSource>();
    //     var cardInfo = cardInteractable.cardInfo;
    //    var owner = gameObject.GetComponent<PlayerCombatantController>();
    //    card.InitializeCard(cardInfo, owner);
    //    cards.Add(cardObj);
    //}
}
