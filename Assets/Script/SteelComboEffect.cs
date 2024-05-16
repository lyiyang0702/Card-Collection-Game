using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelComboEffect : BaseComboEffect
{
    public override void ApplyStatsModEffect(Damageable other, float amount)
    {
        var enemyHP = other.baseHealthPoints;
        if (shouldUpgradeCombo)
        {
            if (!other.isDead)
            {
                other.UpdateHealth(-enemyHP);
            }

            owner.UpdateHealth(enemyHP);

        }
        else
        {
            if (!other.isDead)
            {
                other.UpdateHealth(-enemyHP/2);
            }
            owner.UpdateHealth(enemyHP/2);
        }
        base.ApplyStatsModEffect(other, amount);
    }
}
