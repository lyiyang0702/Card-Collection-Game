using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Difficulty { Easy, Mid, Hard, Boss}
public class EnemyCombatantController : Damageable
{
    public ElementalType elementalType;
    public Difficulty difficulty;
    [SerializeField] int[] rewardWeightedTable;
    [SerializeField] int elementalRewardNum = 4;
    [SerializeField] int randomRewardNum = 1;
    [SerializeField] List<CardScriptableObject> reward = new List<CardScriptableObject>();
    override public void Start()
    {
        base.Start();
        isEnemy = true;
        switch (difficulty)
        {
            case Difficulty.Easy:
                InitializeStats(8, 2, 0);
                InitializeRewardInfo(new int[] { 60, 30, 10, 0 });
                break;
            case Difficulty.Mid:
                InitializeStats(6, 3, 1);
                InitializeRewardInfo(new int[] { 30, 50, 20, 0 });
                break;
            case Difficulty.Hard:
                InitializeStats(20, 3, 0);
                InitializeRewardInfo(new int[] { 10, 40, 40, 10 });
                break;
            case Difficulty.Boss:
                InitializeStats(100, 10, 3);
                InitializeRewardInfo(new int[] { 10, 20, 30, 40 });
                break;
            default:
                break;
        }


    }

    void InitializeRewardInfo(int[] weightTable)
    {
        rewardWeightedTable = weightTable;
    }
    public void Attack()
    {
        Debug.Log("Enemy Attack");
        PlayerController.Instance.playerCombatant.ApplyDamage(stats.atk);
        //CombatManager.Instance.SwicthTurnEevent.Invoke(BattleState.PlayerTurn);

        OnSwitchTurn(BattleState.PlayerTurn);
    }

    public override void OnSwitchTurn(BattleState state)
    {
        base.OnSwitchTurn(state);
        Debug.Log("Switch to player turn");

    }

    void SpawnReward()
    {
        
        for(int i = 0;i<elementalRewardNum;i++)
        {
            reward.Add(ResourceManager.Instance.ReturnWeightedRandomCardByElementalType(rewardWeightedTable, (int)elementalType));
        }
        reward.AddRange(ResourceManager.Instance.ReturnRandomCard(1));
        
        
        foreach(var card in reward)
        {
            PlayerController.Instance.inventory.AddCardObjToInventory(card);
        }
    }

    public override IEnumerator DeathRoutine()
    {
        SpawnReward();
        return base.DeathRoutine();
    }
}
