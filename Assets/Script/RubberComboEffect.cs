using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberComboEffect : BaseComboEffect
{
    // Start is called before the first frame update
    int mouseClickCounter = 0;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {  
            mouseClickCounter++;
            Debug.Log(mouseClickCounter);
        }
    }

    public override void ApplyTimedModEffect(Damageable other)
    {
        base.ApplyTimedModEffect(other);
        owner.stats.atk = mouseClickCounter;
        Debug.Log("Rubber Combo Effect: " + owner.stats.atk);
        mouseClickCounter = 0;
        CombatManager.Instance.enemyCombatant.ApplyDamage(owner.stats.atk);
    }

    public override void ApplyComboEffect(Damageable other, float amount = 0)
    {
        shouldCountDown = true;
        if (shouldUpgradeCombo)
        {
            effectTime = 3f;
        }
        else
        {
            effectTime = 2f;
        }
        base.ApplyComboEffect(other, amount);
    }
}
