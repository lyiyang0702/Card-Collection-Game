using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<CardScriptableObject> cards =  new List<CardScriptableObject>();
    //public int size = 40;
    //public List<GameObject> cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRemainCardsNum()
    {
        return cards.Count;
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
        Debug.Log(card.name + " is removed from inventory: " + cards.Remove(card));
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

    public List<CardScriptableObject>  SortCardByElementalType(List<CardScriptableObject> cards)
    {
        return cards.OrderBy(x => x.elementalType).ToList();
    }

    public List<CardScriptableObject> SortCardByRarity(List<CardScriptableObject> cards)
    {
        return cards.OrderBy(x => x.colorTier).ToList();
    }


    public void SortCardsByElementsThenColorTier()
    {
        List<CardScriptableObject> sortedCards = new List<CardScriptableObject>();
        IEnumerable<IGrouping<ElementalType, CardScriptableObject>> query = SortCardByRarity(cards).GroupBy(card => card.elementalType, cards => cards);
        foreach (IGrouping<ElementalType, CardScriptableObject> cardGroup in query)
        {
            sortedCards.AddRange(SortCardByElementalType(cardGroup.ToList()));
        }
        cards = sortedCards;
    }

    public void SortCardsByRarityThenElements()
    {
        List<CardScriptableObject> sortedCards = new List<CardScriptableObject>();
        IEnumerable<IGrouping<ColorTier, CardScriptableObject>> query = SortCardByElementalType(cards).GroupBy(card => card.colorTier, cards => cards);
        foreach (IGrouping<ColorTier, CardScriptableObject> cardGroup in query)
        {
            sortedCards.AddRange(SortCardByRarity(cardGroup.ToList()));
        }
        cards = sortedCards;
    }
}
