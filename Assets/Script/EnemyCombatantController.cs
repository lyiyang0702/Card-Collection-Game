using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Difficulty { Easy, Mid, Hard, Boss}
public class EnemyCombatantController : Damageable
{
    public ElementalType elementalType;
    public Difficulty difficulty;
    public int rewardBuff = 0;
    [SerializeField] int[] rewardWeightedTable;
    [SerializeField] int elementalRewardNum = 4;
    [SerializeField] int randomRewardNum = 1;
    [SerializeField] List<CardScriptableObject> reward = new List<CardScriptableObject>();
    [SerializeField] Animator anim;
    override public void Start()
    {
        base.Start();
        elementalType = (ElementalType)Random.Range(0,4);
        //Debug.Log(elementalType);
        isEnemy = true;
        switch (difficulty)
        {
            case Difficulty.Easy:
                InitializeStats(8, 2, 0);
                InitializeRewardInfo(new int[] { 40, 30, 20, 10 });
                break;
            case Difficulty.Mid:
                InitializeStats(6, 3, 1);
                InitializeRewardInfo(new int[] { 20, 30, 30, 20 });
                break;
            case Difficulty.Hard:
                InitializeStats(20, 3, 0);
                InitializeRewardInfo(new int[] { 10, 20, 40, 30 });
                break;
            case Difficulty.Boss:
                InitializeStats(100, 10, 3);
                InitializeRewardInfo(new int[] { 10, 20, 30, 40 });
                break;
            default:
                break;
        }

    }

    void InitializeRewardInfo(int[] weightTable)
    {
        rewardWeightedTable = weightTable;
    }
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(2);
        SetBoolFalse("takeDamage");
        Debug.Log("Enemy Attack");
        PlayerController.Instance.playerCombatant.ApplyDamage(stats.atk);
        //CombatManager.Instance.SwicthTurnEevent.Invoke(BattleState.PlayerTurn);

        OnSwitchTurn(BattleState.PlayerTurn);
    }
    public override void OnSwitchTurn(BattleState state)
    {
        base.OnSwitchTurn(state);
        Debug.Log("Switch to player turn");

    }

    public override void OnEnterCombat()
    {
        base.OnEnterCombat();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        transform.position = new Vector3(UIManager.Instance.enemySpot.transform.position.x, UIManager.Instance.enemySpot.transform.position.y + 0.5f, 0);
        
    }

    public override void OnExitCombat(bool isGameOver = false)
    {
        if (isGameOver)
        {
            GetComponent<EnemyInteractable>().currentInteractState = Interactable.InteractState.CanInteract;
            healthPoints = baseHealthPoints;
        }
        base.OnExitCombat();

        
    }
    void SpawnReward()
    {
        CombatManager.Instance.canSwitchTurn = false;
        for(int i = 0;i<elementalRewardNum;i++)
        {
            reward.Add(ResourceManager.Instance.ReturnWeightedRandomCardByElementalType(rewardWeightedTable, (int)elementalType));
        }

        reward.AddRange(ResourceManager.Instance.ReturnRandomCard(randomRewardNum + rewardBuff));
        
        
        foreach(var card in reward)
        {
            PlayerController.Instance.inventory.AddCardObjToInventory(card);
        }
        
        UIManager.Instance.rewardPanelUI.gameObject.SetActive(true);
        UIManager.Instance.rewardPanelUI.PopulateRewardCard(reward);
    }

    public override IEnumerator DeathRoutine()
    {
        if(difficulty == Difficulty.Boss)
        {
            UIManager.Instance.endingPanel.SetActive(true) ;
            
        }
        else
        {
            SpawnReward();
            Debug.Log("Enemy: " + gameObject.name + " is dead");
            UIManager.Instance.quipBannerController.StartBannerQuip("YOU WIN", null, 0.1f, 1f, 0.1f);
            CombatManager.Instance.battleState = BattleState.Won;
        }

        return base.DeathRoutine();
    }

    public void SetBoolTrue(string boolName)
    {
        anim.SetBool(boolName, true);
    }

    public void SetBoolFalse(string boolName)
    {
        anim.SetBool(boolName, false);
    }

}
