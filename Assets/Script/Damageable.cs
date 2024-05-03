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
}
public class Damageable : MonoBehaviour
{
    public string displayName = "Pyoro";
    public float baseHealthPoints;
    public float healthPoints;
    public UnityEvent<float> OnDamageEvent;
    public UnityEvent<float> OnHealthUpdatedEvent;
    public UnityEvent<Damageable> OnDeathEvent;
    public Rigidbody2D rb;
    public bool isPlayer;
    public bool isEnemy;
    public Stats stats = new Stats();

    public int attackBuff;
    public int defenseBuff;
    public int agilityBuff;

    private void Awake()
    {
        healthPoints = baseHealthPoints;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public void ApplyDamage(float atk)
    {
        float dmg = atk * (100 / (100 + stats.def));
        Debug.Log("Cause dmg:" + dmg);
        healthPoints -= dmg;
        OnHealthUpdatedEvent.Invoke(healthPoints);
        if (healthPoints <= 0)
        {
            OnDeathEvent.Invoke(this);
        }
    }



}
