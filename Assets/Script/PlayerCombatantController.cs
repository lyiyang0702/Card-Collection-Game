using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerCombatantController : Damageable
{
    public List<CardDamageSource> cardCombo = new List<CardDamageSource>();
    
    ElementalType comboEffectType;
    override public void Start()
    {
        UIManager.Instance.attackButton.onClick.AddListener(Attack);
        isPlayer = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (CombatManager.Instance.enemy == null) return;
            var enemyCombatant = CombatManager.Instance.enemy.GetComponent<EnemyCombatantController>();
            if(enemyCombatant != null)
            {
                enemyCombatant.ApplyDamage(300);
            }

        }
    }
    public void Attack()
    {
        CalculateDamage(CheckIfHasComboEffect());
        if (CombatManager.Instance.enemy == null) return;
        var enemyCombatant = CombatManager.Instance.enemy.GetComponent<EnemyCombatantController>();
        enemyCombatant.ApplyDamage(stats.atk);
        foreach (var card in cardCombo)
        {
            card.DestroyCard();
        }
        cardCombo.Clear();
        stats.atk = 0;
    }

    public bool CheckIfHasComboEffect()
    {
        var enemyCombatant = CombatManager.Instance.enemy.GetComponent<EnemyCombatantController>();
        var duplicateElements = cardCombo
            .GroupBy(x => x.cardInfo.elementalType)
            .Where(g => g.Count() >=3)
            .Select(g => g.Key);

        foreach (var element in duplicateElements)
        {
            comboEffectType = element;
            switch (element)
            {
                case ElementalType.Steel:
                    healthPoints += 3;
                    enemyCombatant.healthPoints -= 3;
                    break;
                case ElementalType.Titanium:
                    attackBuff = 3;
                    break;
                case ElementalType.Rubber:
                    break;
                case ElementalType.Gold:
                    break;
                default:
                    break;
            }
        }
        
        return duplicateElements.Count() > 0;
    }

    void CalculateDamage(bool hasComboEffect)
    {
        if (!hasComboEffect)
        {
            foreach (var card in cardCombo) {
                stats.atk += card.damage;
            }
        }
        else
        {
            var cards = cardCombo.Where(x => x.cardInfo.elementalType == comboEffectType).ToList();

            foreach (var card in cards)
            {
                stats.atk += card.damage;
            }
        }
        stats.atk += attackBuff;
    }

}
