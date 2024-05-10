using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanelController : MonoBehaviour
{
    public GameObject rewardCardParent;

    
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
}
