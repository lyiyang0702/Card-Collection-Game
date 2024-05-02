using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDamageSource : MonoBehaviour,IPointerClickHandler
{
    public Damageable owner;
    public int baseDamage;
    public int damage;  
    public CardScriptableObject cardInfo;
    EnemyCombatantController enemyCombatant;
    // Start is called before the first frame update

    void Start()
    {
        enemyCombatant = CombatManager.Instance.enemy.GetComponent<EnemyCombatantController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeCard(CardScriptableObject cardInfo, PlayerCombatantController owner)
    {
        this.cardInfo = cardInfo;
        this.owner = owner;
        baseDamage = (int)cardInfo.colorTier;
        damage = baseDamage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance.isSelecting) return;
        Attack();
    }

    private void Attack()
    {
        enemyCombatant.ApplyDamage(damage);
    }
}
