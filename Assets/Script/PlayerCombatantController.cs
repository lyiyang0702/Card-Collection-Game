using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;
public class PlayerCombatantController : Damageable
{
    public List<CardDamageSource> cardCombo = new List<CardDamageSource>();
    public float quipBannerLingearTime = 3f;
    public GameObject playerSprite;
    public AudioSource confirmSound;


    private Animator playerAnim;
    private SpriteRenderer playerSpriteRenderer;
    private BaseComboEffect comboEffect;
    private ElementalType comboEffectType;
    [SerializeField] private AudioClip defaultSFX;
    [SerializeField] private AudioClip goldSFX;
    [SerializeField] private AudioClip rubberSFX;
    [SerializeField] private AudioClip steelSFX;
    [SerializeField] private AudioClip titaniumSFX;
    [SerializeField] private AudioClip errorSFX;
    public AudioSource hitSound;
    [SerializeField] private AudioClip[] enemySFX;

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
        if (Input.GetKeyUp(KeyCode.K))
        {
            var enemyCombatant = CombatManager.Instance.enemyCombatant;
            if(enemyCombatant != null)
            {
                enemyCombatant.ApplyDamage(300);
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
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

        if (enemyCombatant == null)
        {
            Debug.Log("No available enemy to attack");
            return;
        }

        UIManager.Instance.quipBannerController.StopBannerQuip();
        // first, apply card damage
        // TO DO: Add attack ui
        var dmg = CalculateDamage();
        if (dmg == 0) return;
        enemyCombatant.ApplyDamage(dmg);

        if (CombatManager.Instance.enemyCombatant.isDead)
        {
            OnAttackEnd();
            return;
        }
        // then, apply combo effect
        //CheckIfHasComboEffect();
        if (comboEffect != null) {
            comboEffect.ApplyComboEffect(enemyCombatant);
        } 
        OnAttackEnd();

    }

    void OnAttackEnd()
    {
        //Clear card deck and combo list
        UIManager.Instance.ClearCardChildren(UIManager.Instance.selectedCardParent.transform);
        cardCombo.Clear();
        stats.atk = 0;
        if(PlayerController.Instance.inventory.GetRemainCardsNum() == 0 && !CombatManager.Instance.enemyCombatant.isDead)
        {
            StartCoroutine(DeathRoutine());
            return;
        }
        Debug.Log("Player Attack");
        setEnemySFX();
        StartCoroutine(resetSFX());
        OnSwitchTurn(BattleState.EnemyTurn);
    }
    public bool CheckIfHasComboEffect()
    {
        var enemyCombatant = CombatManager.Instance.enemyCombatant;
        bool upgradeCombo = false;
        Dictionary<ElementalType, int> comboDict = new Dictionary<ElementalType, int>();
        confirmSound.clip = defaultSFX;
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
            comboEffect.waitTime = 2f;
            UIManager.Instance.quipBannerController.StartBannerQuip(comboEffect.comboEffectDescription, comboEffect.displayName, 1f, quipBannerLingearTime, 1f);          
        }
        switch(comboEffectType){
            case ElementalType.Gold:
                confirmSound.clip = goldSFX;
            break;

            case ElementalType.Rubber:
                confirmSound.clip = rubberSFX;
            break;

            case ElementalType.Steel:
                confirmSound.clip = steelSFX;
            break;

            case ElementalType.Titanium:
                confirmSound.clip = titaniumSFX;
            break;

            default:
                confirmSound.clip = defaultSFX;
            break;
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
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        PlayerController.Instance.StopAllMovement();
        transform.position = new Vector3(UIManager.Instance.playerSpot.transform.position.x + 0.08f, UIManager.Instance.playerSpot.transform.position.y- 0.5f, 0);
        playerAnim.SetBool("inCombat", true);
        playerSpriteRenderer.flipX = false;
        healthPointsBeforeCombat = healthPoints;
    }

    public override void OnExitCombat(bool isGameOver = false)
    {
        //Debug.Log("HP before combat: " + healthPointsBeforeCombat);
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
        UIManager.Instance.quipBannerController.StartBannerQuip("YOU DIED", null, 0.1f, 2f, 0.1f);
        Debug.Log(gameObject.name + " is dead");
        CombatManager.Instance.battleState = BattleState.Lost;
        return base.DeathRoutine();
    }

    IEnumerator resetSFX(){
        yield return new WaitForSeconds(1.0f);
        confirmSound.clip = errorSFX;
    }

    public void setEnemySFX(){
        int rand = UnityEngine.Random.Range(0,3);
        hitSound.clip = enemySFX[rand];
    }
}
