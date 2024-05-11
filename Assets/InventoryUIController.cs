using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryGrid;
    public Button ConfirmButton;

    private void OnEnable()
    {
        UIManager.Instance.PopulateCardsToTransform(PlayerController.Instance.inventory.cards, inventoryGrid.transform);
        ConfirmButton.onClick.AddListener(OnConfirm);
        UIManager.Instance.canSelectCards = true;
        UIManager.Instance.inventoryButton.interactable = false;
    }

    private void OnDisable()
    {
        UIManager.Instance.ClearCardChildren(inventoryGrid.transform);
        ConfirmButton.onClick.RemoveListener(OnConfirm);
        UIManager.Instance.tempSelectedCards.Clear();
        UIManager.Instance.canSelectCards = false;
    }



    void SubmitSelectedCards()
    {
        var cardParent = UIManager.Instance.selectedCardParent.transform;
        foreach (var card in UIManager.Instance.tempSelectedCards)
        {
            GameObject cardUI = UIManager.Instance.CreateCardUI(card);
            PlayerController.Instance.playerCombatant.cardCombo.Add(cardUI.GetComponent<CardDamageSource>());
            cardUI.transform.SetParent(cardParent);
            cardUI.GetComponent<RectTransform>().localScale = Vector3.one;
            PlayerController.Instance.inventory.RemoveCard(card);
        }

    }

    void OnConfirm()
    {
        SubmitSelectedCards();
        gameObject.SetActive(false);
    }


}
