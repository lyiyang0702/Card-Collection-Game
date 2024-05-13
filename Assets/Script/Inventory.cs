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
        //if (cards.Count >= size)
        //{
        //    Debug.Log("Inventory is full.");
        //    // drop cards in inventory
        //    return;
        //}

        if (GameManager.Instance.isDebug)
        {
            cards.Add(cardInteractable.cardInfo);
            return;
        }

        foreach (CardScriptableObject card in cardInteractable.cardInfos)
        {
            cards.Add(card);
        }
        
    }

    public void AddCardObjToInventory(CardScriptableObject cardObj)
    {
        cards.Add(cardObj);
    }
    public void RemoveCard(CardScriptableObject card)
    {
        if (!cards.Contains(card)) return;
        //Debug.Log(card.name + " is removed from inventory: " + cards.Remove(card));
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
