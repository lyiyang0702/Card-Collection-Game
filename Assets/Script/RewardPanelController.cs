using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardPanelController : MonoBehaviour
{
    public GameObject rewardCardParent;
    public TextMeshProUGUI titleText;


    private void OnEnable()
    {
        if (CombatManager.Instance.battleState == BattleState.None)
        {
            titleText.gameObject.SetActive(false);
            Time.timeScale = 0;
            return;
        }
        titleText.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        UIManager.Instance.ClearCardChildren(rewardCardParent.transform);
        if (CombatManager.Instance.battleState == BattleState.None)
        {
            Time.timeScale = 1;
        }
            
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        CombatManager.Instance.canEndBattle = true;
    }

    public void PopulateRewardCard(List<CardScriptableObject> cardList)
    {
        UIManager.Instance.PopulateCardsToTransform(cardList,rewardCardParent.transform, true);
    }


}
