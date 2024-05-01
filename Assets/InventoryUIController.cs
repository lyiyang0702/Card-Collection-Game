using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject cardOrganizer;

    private void OnEnable()
    {
        PopulateCardUI();
    }

    private void OnDisable()
    {
        ClearCards();
    }

    private void ClearCards()
    {
        for (int i = 0; i < cardOrganizer.transform.childCount; i++)
        {
            var cardSlot = cardOrganizer.transform.GetChild(i);
            for (int j = 0; j < cardSlot.transform.childCount; j++)
            {
                Destroy(cardSlot.transform.GetChild(j));
            }
        }
    }

    void PopulateCardUI()
    {
        for(int i = 0; i< PlayerController.Instance.inventory.cards.Count; i++)
        {
           // int cardSlotIndex = 
        }

    }

}
