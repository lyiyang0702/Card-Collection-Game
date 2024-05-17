using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Stats
{
    public float atk;
    public float spd;
    public float def;

    public void ResetStat()
    {
        atk = 0; spd = 0; def = 0;
    }
}
public class Damageable : MonoBehaviour
{
    public FlashEffect flashEffect;
    public float dealtDmg = 0;
    public string displayName = "Pyoro";
    public float baseHealthPoints;
    public float healthPoints;
    public float healthPointsBeforeCombat;
    public Vector3 posBeforeCombat;
    public UnityEvent<float> OnDamageEvent;
    public UnityEvent<float> OnHealthUpdatedEvent;
    public UnityEvent<Damageable> OnDeathEvent;
    public Rigidbody2D rb;
    public bool isPlayer;
    public bool isEnemy;
    public Stats stats = new Stats();
    public bool isDead = false;
    public int attackBuff;
    public int defenseBuff;
    public int agilityBuff;

    private void Awake()
    {
        healthPoints = baseHealthPoints;
        flashEffect = GetComponent<FlashEffect>();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //CombatManager.Instance.SwicthTurnEevent.AddListener(OnSwitchTurn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStats(float hp, float atk, float def, float spd = 0)
    {
        baseHealthPoints = hp;
        healthPoints = baseHealthPoints;
        stats.atk = atk;
        stats.spd = spd;
        stats.def = def;
    }

    public void ApplyDamage(float dmg)
    {
        UpdateHealth(-dmg);
        OnDamageEvent?.Invoke(dmg);
        Debug.Log("Cause dmg:" + dmg);
        flashEffect.Flash();

    }

    public void UpdateHealth(float healthChange)
    {
        healthPoints += healthChange;
        healthPoints = Mathf.Clamp(healthPoints, 0, 10000);
        OnHealthUpdatedEvent?.Invoke(healthPoints);
        if (Mathf.RoundToInt(healthPoints) < 1)
        {

            StartCoroutine(DeathRoutine());
        }

    }
    public virtual IEnumerator DeathRoutine()
    {
        CombatManager.Instance.canSwitchTurn = false;
        isDead = true;


        Debug.Log("Start Death Routine");
        yield return new WaitUntil(() => CombatManager.Instance.canEndBattle == true);
        OnDeathEvent?.Invoke(this);

    }

    virtual public void OnSwitchTurn(BattleState state)
    {
        if (CombatManager.Instance.battleState == BattleState.Lost || CombatManager.Instance.battleState == BattleState.Won) return;
        CombatManager.Instance.battleState = state;
        CombatManager.Instance.SwicthTurnEevent.Invoke(state);
    }

    public void ResetStat()
    {
        attackBuff = 0;
        defenseBuff = 0;
        agilityBuff = 0;
        stats.ResetStat();

    }

    virtual public void OnEnterCombat()
    {
        
        posBeforeCombat = transform.position;
        healthPointsBeforeCombat = healthPoints;
    }

    virtual public void OnExitCombat(bool isGameOver = false)
    {
        isDead = false;
        transform.localScale = Vector3.one;
        transform.position = posBeforeCombat;
   
    }
}
