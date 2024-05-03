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
                InitializeStats(6, 2, 0);
                break;
            case Difficulty.Mid:
                InitializeStats(10, 2, 1);
                break;
            case Difficulty.Hard:
                InitializeStats(15, 5, 2);
                break;
            case Difficulty.Boss:
                InitializeStats(30, 10, 5);
                break;
            default:
                break;
        }
        CombatManager.Instance.SwicthTurnEevent.AddListener(Attack);
    }

    public void Attack()
    {
        if (CombatManager.Instance.battleState != BattleState.EnemyTurn) return;
        PlayerController.Instance.playerCombatant.ApplyDamage(stats.atk);
    }

}
