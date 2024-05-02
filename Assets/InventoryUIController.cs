using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryGrid;
    public GameObject CardUIPrefab;

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
        for (int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            Destroy(inventoryGrid.transform.GetChild(i));
        }
    }

    void PopulateCardUI()
    {
        foreach (var card in PlayerController.Instance.inventory.cards)
        {
            GameObject cardObj = Instantiate(CardUIPrefab);
            cardObj.GetComponent<CardUI>().UpdateCardUI(card);
            cardObj.transform.SetParent(inventoryGrid.transform);
        }

    }

}
