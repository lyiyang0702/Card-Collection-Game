using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<CardScriptableObject> interactables = new List<CardScriptableObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInteractableToInventory(Interactable interactable)
    {
        interactables.Add(interactable.cardObj);
    }
}
