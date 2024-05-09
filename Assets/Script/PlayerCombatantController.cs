using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class PlayerCombatantController : Damageable
{
    public List<CardDamageSource> cardCombo = new List<CardDamageSource>();
    int mouseClickCounter = 0;
    bool shouldCountClick = false;
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

        if (Input.GetMouseButtonDown(0))
        {
            if (!shouldCountClick) return;
            mouseClickCounter++;
            Debug.Log(mouseClickCounter);
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
        CalculateDamage();
        if (stats.atk == 0) return;
        enemyCombatant.ApplyDamage(stats.atk);

        //StartCoroutine(ApplyComboEffect());
        CheckIfHasComboEffect();
        //Clear card deck and combo list
        UIManager.Instance.ClearCardDeck();
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
        foreach (var card in cardCombo.GroupBy(x => x.cardInfo.elementalType))
        {
            comboDict[card.Key] = card.Count();
        }
        foreach (var element in comboDict) {
            if (element.Value < 3) return false;
            comboEffectType = element.Key;
            upgradeCombo = element.Value == 5;

            Debug.Log("Combo Effect: " + comboEffectType + ", Value: " + element.Value);

            //Apply Combo Effect
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
                    if (upgradeCombo)
                    {
                        enemyCombatant.defenseBuff = - (int) enemyCombatant.stats.def;
                    }
                    else
                    {
                        enemyCombatant.defenseBuff = -(int)enemyCombatant.stats.def / 2;
                    }
                    break;
                case ElementalType.Rubber:
                    float effectTime;
                    
                    if (upgradeCombo)
                    {
                        effectTime = 3f;
                    }
                    else
                    {
                        effectTime = 2f;
                    }

                    StartCoroutine(RubberComboEffect(effectTime));
                    break;
                case ElementalType.Gold:
                    if (upgradeCombo)
                    {
                        enemyCombatant.rewardBuff = 4;
                    }
                    else
                    {
                        enemyCombatant.rewardBuff = 2;
                    }
                    break;
                default:
                    break;
            }
        }

        return comboDict.Count > 0;
    }

    IEnumerator RubberComboEffect(float waitTime)
    {
        shouldCountClick = true;
        CombatManager.Instance.canSwitchTurn = false;
        yield return new WaitForSeconds(waitTime);
        shouldCountClick = false;
        stats.atk = mouseClickCounter;
        Debug.Log("Rubber Combo Effect: " + stats.atk);
        mouseClickCounter = 0;
        CombatManager.Instance.enemyCombatant.ApplyDamage(stats.atk);
        CombatManager.Instance.canSwitchTurn = true;

    }
    void CalculateDamage()
    {
        foreach (var card in cardCombo)
        {
            stats.atk += card.damage;
        }
        //if (!hasComboEffect)
        //{

        //}
        //else
        //{
        //    var cards = cardCombo.Where(x => x.cardInfo.elementalType == comboEffectType).ToList();

        //    foreach (var card in cards)
        //    {
        //        stats.atk += card.damage;
        //    }
        //}
        //stats.atk += attackBuff;
    }




}
