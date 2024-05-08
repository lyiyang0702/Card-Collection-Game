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
        //foreach (var card in cardCombo)
        //{
        //    card.DestroyCard();
        //}
        cardCombo.Clear();
        stats.atk = 0;
        Debug.Log("Player Attack");
        OnSwitchTurn(BattleState.EnemyTurn);
        //CombatManager.Instance.SwicthTurnEevent?.Invoke(BattleState.EnemyTurn);
    }

    public bool CheckIfHasComboEffect()
    {
        var enemyCombatant = CombatManager.Instance.enemyCombatant;
        bool upgradeCombo = false;
        Dictionary<ElementalType, int> comboDict = new Dictionary<ElementalType, int>();
        //var duplicateElements = cardCombo
        //    .GroupBy(x => x.cardInfo.elementalType)
        //    .Where(g => g.Count() >=3)
        //    .Select(g => g.Key).ToList();
        foreach (var card in cardCombo.GroupBy(x => x.cardInfo.elementalType))
        {
            comboDict[card.Key] = card.Count();
        }
        foreach (var element in comboDict) {
            if (element.Value < 3) return false;
            comboEffectType = element.Key;
            upgradeCombo = element.Value == 5;

            Debug.Log("Combo Effect: " + comboEffectType + ", Value: " + element.Value);
            switch (comboEffectType)
            {
                case ElementalType.Steel:
                    if (upgradeCombo)
                    {
                        UpdateHealth(enemyCombatant.healthPoints);
                        enemyCombatant.UpdateHealth(-enemyCombatant.healthPoints);
                    }
                    else
                    {
                        UpdateHealth(enemyCombatant.healthPoints/2);
                        enemyCombatant.UpdateHealth(-enemyCombatant.healthPoints/2);
                    }
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

        return comboDict.Count > 0;
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
