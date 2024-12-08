using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelComboEffect : BaseComboEffect
{
    public override void ApplyStatsModEffect(Damageable other, float amount)
    {
        
        if (shouldUpgradeCombo)
        {

            owner.UpdateHealth(owner.dealtDmg);

        }
        else
        {

            owner.UpdateHealth(owner.dealtDmg / 2);
        }
        base.ApplyStatsModEffect(other, amount);
    }
}
