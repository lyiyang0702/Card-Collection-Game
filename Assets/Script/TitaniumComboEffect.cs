using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitaniumComboEffect : BaseComboEffect
{
    public override void ApplyStatsModEffect(Damageable other, float amount = 0)
    {
        CombatManager.Instance.canSwitchTurn = false;
        Debug.Log("Apply Titanium");
        if (shouldUpgradeCombo)
        {
            other.defenseBuff = -(int)other.stats.def;
        }
        else
        {
            other.defenseBuff = -(int)other.stats.def / 2;
        }
        CombatManager.Instance.canSwitchTurn = true;
        Destroy(gameObject);
    }
}
