using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseComboEffect : MonoBehaviour
{
    public string displayName;
    public string comboEffectDescription;
    public bool applyToDeadEnemy = false;
    public enum EffectType { Timed, StatsMod, Other};
    public enum ModEffectType { Atk, Def, Health};
    public EffectType effectType;
    public ElementalType elementalType;
    public bool shouldUpgradeCombo = false;
    [SerializeField] float effecTimer = 0f;
    [SerializeField] ModEffectType modEffectType;
    protected float effectTime = 0f;
    protected bool shouldCountDown = false;
    public float waitTime = 4f;
    public Damageable owner;
    public UnityEvent countDownEndEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    virtual public void Update()
    {
        //Debug.Log("Count down started: " + shouldCountDown);

        CountDownTimer();
    }

    virtual public void ApplyStatsModEffect(Damageable other,float amount = 0)
    {
        
    }

    virtual public void ApplyOtherEffect(Damageable other)
    {
       
    }
    virtual public void ApplyTimedModEffect(Damageable other)
    {
        //Debug.Log("Apply Timed Mod Effect");
        shouldCountDown = true;
    }
    virtual public void ApplyComboEffect(Damageable other, float amount = 0)
    {
        StartCoroutine(ComboEffectRoutine(waitTime,other,amount));

    }

    IEnumerator ComboEffectRoutine(float waitTime,Damageable other, float amount = 0)
    {
        CombatManager.Instance.canSwitchTurn = false;
        
        yield return new WaitForSecondsRealtime(waitTime);
        Debug.Log("Apply Combo Effect");
        switch (effectType)
        {
            case EffectType.Timed:
                ApplyTimedModEffect(other);
                break;
            case EffectType.StatsMod:
                ApplyStatsModEffect(other, amount);
                CombatManager.Instance.canSwitchTurn = true;
                Destroy(gameObject);
                break;
            case EffectType.Other:
                ApplyOtherEffect(other);
                CombatManager.Instance.canSwitchTurn = true;
                Destroy(gameObject);
                break;
        }
        

    }

    virtual public void CountDownTimer()
    {
        if (shouldCountDown)
        {
            Debug.Log("Time elapsed: " + effecTimer);
            effecTimer += Time.deltaTime;
            if (effecTimer > effectTime)
            {
                effecTimer = 0f;
                countDownEndEvent.Invoke();
            }
        }
    }

}
