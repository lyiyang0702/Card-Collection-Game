using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitaniumComboEffect : BaseComboEffect
{
    public override void ApplyStatsModEffect(Damageable other, float amount = 0)
    {
        if (shouldUpgradeCombo)
        {
            other.defenseBuff = -(int)other.stats.def;
        }
        else
        {
            other.defenseBuff = -(int)other.stats.def / 2;
        }
    }
}
