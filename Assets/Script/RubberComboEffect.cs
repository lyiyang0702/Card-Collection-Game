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
        if (Input.GetMouseButtonDown(0) && shouldCountDown)
        {  
            mouseClickCounter++;
            Debug.Log(mouseClickCounter);
        }
    }

    public override void ApplyTimedModEffect(Damageable other)
    {
        base.ApplyTimedModEffect(other);
        //Debug.Log("should count down: " + shouldCountDown);
        countDownEndEvent.AddListener(OnCountDownEnd);
    }

    public override void ApplyComboEffect(Damageable other, float amount = 0)
    {
       
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

    void OnCountDownEnd()
    {
        shouldCountDown = false;
        owner.stats.atk = mouseClickCounter;
        Debug.Log("Rubber Combo Effect: " + owner.stats.atk);
        CombatManager.Instance.enemyCombatant.ApplyDamage(owner.stats.atk);
        mouseClickCounter = 0;
        //CombatManager.Instance.canSwitchTurn = true;
        Destroy(gameObject);
    }
}
