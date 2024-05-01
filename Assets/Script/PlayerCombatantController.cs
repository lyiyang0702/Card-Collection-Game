using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatantController : Damageable
{
    public List<CardScriptableObject> cards = new List<CardScriptableObject>();
    public List<GameObject> selectedCars = new List<GameObject>();
    override public void Start()
    {
        cards = PlayerController.Instance.inventory.cards;
    }


    public void SelectCards()
    {

    }
}
