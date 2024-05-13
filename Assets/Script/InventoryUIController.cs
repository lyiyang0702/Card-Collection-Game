using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryGrid;
    public Button ConfirmButton;
    public TextMeshProUGUI sortTip;
    bool isDeafultSort = false;
    private void OnEnable()
    {
        SortCards();

        ConfirmButton.onClick.AddListener(OnConfirm);
        UIManager.Instance.canSelectCards = true;
        UIManager.Instance.inventoryButton.interactable = false;

        if (CombatManager.Instance.battleState == BattleState.None)
        {
            ConfirmButton.gameObject.SetActive(false);
        }
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
        PlayerController.Instance.playerCombatant.CheckIfHasComboEffect();
    }

    void OnConfirm()
    {
        SubmitSelectedCards();
        gameObject.SetActive(false);
    }


    public void SortCards()
    {
        isDeafultSort = !isDeafultSort;
        //Debug.Log("Is Deafult Sort: " + isDeafultSort);
         UIManager.Instance.ClearCardChildren(inventoryGrid.transform);
        if (isDeafultSort)
        {
            PlayerController.Instance.inventory.SortCardsByElementsThenColorTier();
            sortTip.text = "Elemental Type";
        }
        else
        {
            PlayerController.Instance.inventory.SortCardsByRarityThenElements();
            sortTip.text = "Rarity";
        }
        UIManager.Instance.PopulateCardsToTransform(PlayerController.Instance.inventory.cards, inventoryGrid.transform);
    }

}
