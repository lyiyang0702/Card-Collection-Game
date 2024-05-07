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

    public List<CardScriptableObject> ReturnRandomCard( int tier, int number  = 1)
    {
        
        List<CardScriptableObject> generatedCards = new List<CardScriptableObject>();

        var tierCard = (
            from card in cardList
            where (int)card.colorTier == tier
            select card
            ).ToList();

        //foreach (var card in tierCard)
        //{
        //    Debug.Log(card.name);
        //}
        for (int i = 0; i < number; i++)
        {
            int rng = Random.Range(0, 5);
            generatedCards.Add(tierCard[rng]);
        }
        return generatedCards;
    }
}
