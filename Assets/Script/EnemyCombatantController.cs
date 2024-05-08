using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Mid, Hard, Boss}
public class EnemyCombatantController : Damageable
{
    public ElementalType elementalType;
    public Difficulty difficulty;
    
    override public void Start()
    {
        base.Start();
        isEnemy = true;
        switch (difficulty)
        {
            case Difficulty.Easy:
                InitializeStats(8, 2, 0);
                break;
            case Difficulty.Mid:
                InitializeStats(6, 3, 1);
                break;
            case Difficulty.Hard:
                InitializeStats(20, 3, 0);
                break;
            case Difficulty.Boss:
                InitializeStats(100, 10, 3);
                break;
            default:
                break;
        }


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


}
