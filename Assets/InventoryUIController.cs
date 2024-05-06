using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryGrid;
    public GameObject CardUIPrefab;
    public Button ConfirmButton;

    private void OnEnable()
    {
        PopulateCardUI();
        ConfirmButton.onClick.AddListener(OnConfirm);
        UIManager.Instance.canSelectCards = true;
    }

    private void OnDisable()
    {
        ClearCards();
        ConfirmButton.onClick.RemoveListener(OnConfirm);
        UIManager.Instance.tempSelectedCards.Clear();
        UIManager.Instance.canSelectCards = false;
    }

    private void ClearCards()
    {
        for (int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            Destroy(inventoryGrid.transform.GetChild(i).gameObject);
        }
    }

    void PopulateCardUI()
    {
        foreach (var card in PlayerController.Instance.inventory.cards)
        {
            CreateCardUI(card).transform.SetParent(inventoryGrid.transform);
        }

    }

    void SubmitSelectedCards()
    {
        var cardParent = UIManager.Instance.selectedCardParent.transform;
        foreach (var card in UIManager.Instance.tempSelectedCards)
        {
            GameObject cardUI = CreateCardUI(card);
            PlayerController.Instance.playerCombatant.cardCombo.Add(cardUI.GetComponent<CardDamageSource>());
            cardUI.transform.SetParent(cardParent);
            cardUI.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void OnConfirm()
    {
        SubmitSelectedCards();
        gameObject.SetActive(false);
    }

    GameObject CreateCardUI(CardScriptableObject cardInfo)
    {
        GameObject cardObj = Instantiate(CardUIPrefab);
        cardObj.GetComponent<CardUI>().UpdateCardUI(cardInfo);
        cardObj.GetComponent<CardDamageSource>().InitializeCard(cardInfo, PlayerController.Instance.playerCombatant);
        
        return cardObj;
    }
}
