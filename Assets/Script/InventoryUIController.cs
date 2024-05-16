using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUIController : MonoBehaviour
{
    public GameObject comboTutorialPanel;
    public GameObject inventoryGrid;
    public Button ConfirmButton;
    public TextMeshProUGUI sortTip;
    bool isDeafultSort = false;
    [SerializeField]bool isConfirmed = false;

    private void Start()
    {
        if (PlayerController.Instance.inventory.GetRemainCardsNum() > 0 && GameManager.Instance.currentArea == 0 && CombatManager.Instance.battleState != BattleState.None)
        {
            comboTutorialPanel.SetActive(true);
        }
    }
    private void OnEnable()
    {
        
        SortCards();


        ConfirmButton.onClick.AddListener(OnConfirm);
        UIManager.Instance.canSelectCards = true;
        UIManager.Instance.inventoryButton.interactable = false;

        if (CombatManager.Instance.battleState == BattleState.None)
        {
            ConfirmButton.gameObject.SetActive(false);
            return;
        }
        ConfirmButton.gameObject.SetActive(true);

    }

    private void OnDisable()
    {
        isConfirmed = false;
        UIManager.Instance.ClearCardChildren(inventoryGrid.transform);
        ConfirmButton.onClick.RemoveListener(OnConfirm);
        UIManager.Instance.tempSelectedCards.Clear();
        UIManager.Instance.canSelectCards = false;
    }



    void SubmitSelectedCards()
    {
        isConfirmed = true;
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
        if (UIManager.Instance.tempSelectedCards.Count == 0)
        {
            UIManager.Instance.quipBannerController.StartBannerQuip("No card selected!", null, 0.1f, 1f, 0.1f);
            return;
        }
        SubmitSelectedCards();
        gameObject.SetActive(false);
    }


    public void SortCards()
    {
        isDeafultSort = !isDeafultSort;
        //Debug.Log("Is Deafult Sort: " + isDeafultSort);
         UIManager.Instance.ClearCardChildren(inventoryGrid.transform);
        UIManager.Instance.tempSelectedCards.Clear();
        if (isDeafultSort)
        {
            PlayerController.Instance.inventory.SortCardsByElementsThenColorTier();
            sortTip.text = "SORT BY ELEMENT";
        }
        else
        {
            PlayerController.Instance.inventory.SortCardsByRarityThenElements();
            sortTip.text = "SORT BY RARITY";
        }
        UIManager.Instance.PopulateCardsToTransform(PlayerController.Instance.inventory.cards, inventoryGrid.transform,false);

    }

    public void CloseInventory()
    {
        if (UIManager.Instance.tempSelectedCards.Count == 0 && CombatManager.Instance.battleState != BattleState.None)
        {
            UIManager.Instance.quipBannerController.StartBannerQuip("No card selected!", null, 0.1f, 1f, 0.1f);
            return;
        }
        gameObject.SetActive(false);
        if (!isConfirmed)
        {
            UIManager.Instance.inventoryButton.interactable = true;
        }
    }
}
