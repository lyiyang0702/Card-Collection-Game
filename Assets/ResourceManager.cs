using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

        return cardList;
    }
}
