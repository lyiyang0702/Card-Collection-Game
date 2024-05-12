using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;
public class PlayerCombatantController : Damageable
{
    public List<CardDamageSource> cardCombo = new List<CardDamageSource>();
    [SerializeField] BaseComboEffect comboEffect;
    public float quipBannerLingearTime = 3f;
    public GameObject playerSprite;
    private Animator playerAnim;
    private SpriteRenderer playerSpriteRenderer;

    ElementalType comboEffectType;
    override public void Start()
    {
        playerAnim = playerSprite.GetComponent<Animator>();
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
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

        // first, apply card damage
        // TO DO: Add attack ui
        var dmg = CalculateDamage();
        if (dmg == 0) return;
        enemyCombatant.ApplyDamage(dmg);

        // then, apply combo effect
        CheckIfHasComboEffect();

        OnAttackEnd();

    }

    void OnAttackEnd()
    {
        //Clear card deck and combo list
        UIManager.Instance.ClearCardChildren(UIManager.Instance.selectedCardParent.transform);
        cardCombo.Clear();
        stats.atk = 0;
        Debug.Log("Player Attack");
        OnSwitchTurn(BattleState.EnemyTurn);
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
            comboEffect = Instantiate(CombatManager.Instance.comboEffectDict[comboEffectType]).GetComponent<BaseComboEffect>(); 
            comboEffect.owner = this;
            comboEffect.shouldUpgradeCombo = upgradeCombo;
            comboEffect.waitTime = quipBannerLingearTime;
            UIManager.Instance.quipBannerController.StartBannerQuip(comboEffect.comboEffectDescription, comboEffect.displayName, 1f, quipBannerLingearTime, 1f);
            comboEffect.ApplyComboEffect(enemyCombatant);
        }

        return comboDict.Count > 0;
    }


    float CalculateDamage()
    {
        foreach (var card in cardCombo)
        {
            stats.atk += card.damage;
        }

        return stats.atk;
    }

    public override void OnEnterCombat()
    {
        base.OnEnterCombat();
        PlayerController.Instance.StopAllMovement();
        transform.position = new Vector3(UIManager.Instance.playerSpot.transform.position.x, UIManager.Instance.playerSpot.transform.position.y, 0);
        playerAnim.SetBool("inCombat", true);
        playerSpriteRenderer.flipX = false;
        healthPointsBeforeCombat = healthPoints;
    }

    public override void OnExitCombat(bool isGameOver = false)
    {
        Debug.Log("HP before combat: " + healthPointsBeforeCombat);
        if(isGameOver )
        {
            healthPoints = healthPointsBeforeCombat;
        }
        
        playerAnim.SetBool("inCombat", false);
        StopAllCoroutines();
        base.OnExitCombat();

    }

    public override IEnumerator DeathRoutine()
    {
        CombatManager.Instance.canEndBattle = true;
        return base.DeathRoutine();
    }
}
