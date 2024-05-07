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
        // Debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            var enemyCombatant = CombatManager.Instance.enemyCombatant;
            if(enemyCombatant != null)
            {
                enemyCombatant.ApplyDamage(300);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CombatManager.Instance.playerCombatant != null)
            {
                CombatManager.Instance.playerCombatant.ApplyDamage(300);
            }
        }

    }
    public void Attack()
    {
        var enemyCombatant = CombatManager.Instance.enemyCombatant;

        if(enemyCombatant == null)
        {
            Debug.Log("No available enemy to attack");
            return;
        }
        CalculateDamage(CheckIfHasComboEffect());
        if (stats.atk == 0) return;
        enemyCombatant.ApplyDamage(stats.atk);
        foreach (var card in cardCombo)
        {
            card.DestroyCard();
        }
        cardCombo.Clear();
        stats.atk = 0;
        Debug.Log("Player Attack");
        OnSwitchTurn(BattleState.EnemyTurn);
        //CombatManager.Instance.SwicthTurnEevent?.Invoke(BattleState.EnemyTurn);
    }

    public bool CheckIfHasComboEffect()
    {
        var enemyCombatant = CombatManager.Instance.enemyCombatant;
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
