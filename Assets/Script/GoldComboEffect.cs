using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldComboEffect : BaseComboEffect
{
    public override void ApplyOtherEffect(Damageable other)
    {
        if (shouldUpgradeCombo)
        {
            ((EnemyCombatantController)other).rewardBuff = 4;
        }
        else
        {
            ((EnemyCombatantController)other).rewardBuff = 2;
        }
    }
}
