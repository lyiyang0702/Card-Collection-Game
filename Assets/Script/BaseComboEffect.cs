using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComboEffect : MonoBehaviour
{
    public enum EffectType { Timed, StatsMod, Other};
    public enum ModEffectType { Atk, Def, Health};
    public EffectType effectType;
    public ElementalType elementalType;
    public bool shouldUpgradeCombo = false;

    [SerializeField] float effecTimer = 0f;
    [SerializeField] ModEffectType modEffectType;
    protected float effectTime = 0f;
    protected bool shouldCountDown = false;
    float waitTime = 4f;
    float timer = 0;
    public Damageable owner;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    virtual public void Update()
    {
        //Debug.Log("Count down started: " + shouldCountDown);
        if(shouldCountDown)
        {
            //Debug.Log("Time elapsed: " + effecTimer);
            effecTimer += Time.deltaTime;
            if(effecTimer > effectTime)
            {
                effecTimer = 0f;
                shouldCountDown = false;
            }
        }
    }

    virtual public void ApplyStatsModEffect(Damageable other,float amount = 0)
    {
        
    }

    virtual public void ApplyOtherEffect(Damageable other)
    {
       
    }
    virtual public void ApplyTimedModEffect(Damageable other)
    {
        Debug.Log("Apply Timed Mod Effect");
        
    }
    virtual public void ApplyComboEffect(Damageable other, float amount = 0)
    {
        StartCoroutine(ComboEffectRoutine(waitTime,other,amount));

    }

    IEnumerator ComboEffectRoutine(float waitTime,Damageable other, float amount = 0)
    {
        CombatManager.Instance.canSwitchTurn = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Apply Combo Effect");
        switch (effectType)
        {
            case EffectType.Timed:
                ApplyTimedModEffect(other);
                break;
            case EffectType.StatsMod:
                ApplyStatsModEffect(other, amount);
                break;
            case EffectType.Other:
                ApplyOtherEffect(other);
                break;
        }
        CombatManager.Instance.canSwitchTurn = true;
        Destroy(gameObject);
    }
}
