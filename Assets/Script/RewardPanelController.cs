using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardPanelController : MonoBehaviour
{
    public GameObject rewardCardParent;
    public TextMeshProUGUI titleText;

    private void Start()
    {
        titleText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        UIManager.Instance.ClearCardChildren(rewardCardParent.transform);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        CombatManager.Instance.canEndBattle = true;
    }

    public void PopulateRewardCard(List<CardScriptableObject> cardList)
    {
        UIManager.Instance.PopulateCardsToTransform(cardList,rewardCardParent.transform);
    }

    private void OnEnable()
    {
        if(CombatManager.Instance.battleState != BattleState.None)
        {
            titleText.gameObject.SetActive(true);
        }
    }
}
