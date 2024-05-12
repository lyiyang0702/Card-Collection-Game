using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelComboEffect : BaseComboEffect
{
    public override void ApplyStatsModEffect(Damageable other, float amount)
    {
        var enemyHP = other.healthPoints;
        if (shouldUpgradeCombo)
        {
            other.UpdateHealth(-enemyHP);
            owner.UpdateHealth(enemyHP);

        }
        else
        {
            other.UpdateHealth(-enemyHP/2);
            owner.UpdateHealth(enemyHP/2);
        }
        base.ApplyStatsModEffect(other, amount);
    }
}
