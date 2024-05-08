using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
public class ResourceManager : UnitySingleton<ResourceManager>
{
    
    public List<CardScriptableObject> cardList = new List<CardScriptableObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<CardScriptableObject> ReturnRandomCardByTier( int tier, int number  = 1)
    {
        Debug.Log(number);
        List<CardScriptableObject> generatedCards = new List<CardScriptableObject>();

        var tierCard = (
            from card in cardList
            where (int)card.colorTier == tier
            select card
            ).ToList();

        for (int i = 0; i < number; i++)
        {
            int rng = Random.Range(0, 5);
            generatedCards.Add(tierCard[rng]);
        }
        return generatedCards;
    }

    public List<CardScriptableObject> ReturnRandomCardByElementalType(int elementalType, int number = 1)
    {
        List<CardScriptableObject> generatedCards = new List<CardScriptableObject>();

        var elementalCard = (
            from card in cardList
            where (int)card.elementalType == elementalType
            select card
            ).ToList();
        for (int i = 0; i < number; i++)
        {
            int rng = Random.Range(0, 5);
            generatedCards.Add(elementalCard[rng]);
        }
        return generatedCards;
    }

}
