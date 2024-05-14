using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;
using System;

public enum BattleState {None, Start, PlayerTurn, EnemyTurn, Won, Lost}
public class CombatManager : UnitySingleton<CombatManager>
{
    public BattleState battleState = BattleState.None;
    public UnityEvent<BattleState> SwicthTurnEevent;
    public PlayerCombatantController playerCombatant;
    public EnemyCombatantController enemyCombatant;
    public bool canSwitchTurn = true;
    public bool canEndBattle = false;
    [SerializeField]List<GameObject> comboEffects;
    public Dictionary <ElementalType, GameObject> comboEffectDict = new Dictionary<ElementalType, GameObject>();


    private void Start()
    {
       
        foreach (var effect in comboEffects)
        {
            comboEffectDict[effect.GetComponent<BaseComboEffect>().elementalType] = effect;
        }
        DontDestroyOnLoad(gameObject);
        playerCombatant = PlayerController.Instance.playerCombatant;
        SceneManager.sceneLoaded += OnBattleSceneLoaded;
        SceneManager.sceneUnloaded += OnBattleSceneUnloaded;
        SwicthTurnEevent.AddListener(OnSwitchTurn);
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            if (!SetUpCombatants())
            {
                Debug.Log("NO ENEMY COMBATANT OR PLAYER COMBATANT");
                return;
            }

            DecideTurn();
            InitializeCombatScene();

        }
    }

    private void OnBattleSceneUnloaded(Scene scene)
    {
        if (scene.name == "BattleScene")
        {
            
            ResetExplorationScene();
        }
    }
    private void DecideTurn()
    {
        
        if (enemyCombatant.difficulty == Difficulty.Hard || enemyCombatant.difficulty == Difficulty.Boss)
        {
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            battleState = BattleState.PlayerTurn;
        }

        SwicthTurnEevent?.Invoke(battleState);
    }

    void OnSwitchTurn(BattleState state)
    {
        UIManager.Instance.UpdateBattleUI(state);
        Debug.Log("Can Switch Turn: " + canSwitchTurn);
        StartCoroutine(SwicthTurnRoutine());
    }

    void OnEndBattle(Damageable damageable)
    {
        if (damageable.isEnemy)
        {
            Destroy(damageable.gameObject);
            enemyCombatant = null;
        }
        else if (damageable.isPlayer)
        {
            enemyCombatant.OnDeathEvent.RemoveListener(OnEndBattle);
            // Restart Game?
        }

        playerCombatant.OnDeathEvent.RemoveListener(OnEndBattle);

        SceneManager.UnloadSceneAsync("BattleScene");

    }

    bool SetUpCombatants()
    {
        Debug.Log("Set up combatants");
        if (playerCombatant == null) return false;
        if (enemyCombatant == null) return false;
        playerCombatant.OnDeathEvent.AddListener(OnEndBattle);
        enemyCombatant.OnDeathEvent.AddListener(OnEndBattle);
        UIManager.Instance.enemyAtkText.text = enemyCombatant.stats.atk.ToString();
        UIManager.Instance.enemyDefText.text = enemyCombatant.stats.def.ToString();
        return true;
    }

    public void InitializeCombatScene()
    {

        canEndBattle = false;
        //PlayerController.Instance.StopAllMovement();
        canSwitchTurn = true;

        Camera.main.transform.position = new Vector3(0, 0, -10);
        UIManager.Instance.OnBattleSceneLoaded();
       
        playerCombatant.OnEnterCombat();
        enemyCombatant.OnEnterCombat();
    }

    public void ResetExplorationScene()
    {
        if (playerCombatant == null) return;
        Debug.Log("Reset scene");
        var isGameOver = battleState == BattleState.Lost;
      
        if (isGameOver)
        {
            enemyCombatant.OnExitCombat(isGameOver);
        }
        playerCombatant.OnExitCombat(isGameOver);
        battleState = BattleState.None;
        UIManager.Instance.OnBattleSceneUnLoaded();
    }

    IEnumerator SwicthTurnRoutine()
    {
        yield return new WaitUntil(()=> canSwitchTurn == true);
        //yield return new WaitForSeconds(10);
        if (battleState == BattleState.EnemyTurn)
        {
            enemyCombatant.Attack();
        }
    }
}
