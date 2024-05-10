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
    [SerializeField] Vector3 playerPosBeforeCombat;
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
        StartCoroutine(SwicthTurnRoutine());
    }

    void OnEndBattle(Damageable damageable)
    {
        if (damageable.isEnemy)
        {
            Debug.Log("Enemy: " + damageable.name + " is dead");
            battleState = BattleState.Won;
            Destroy(damageable.gameObject);
            enemyCombatant = null;
        }
        else if (damageable.isPlayer)
        {
            Debug.Log(damageable.name + " is dead");
            battleState = BattleState.Lost;
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

        return true;
    }

    public void InitializeCombatScene()
    {
        canEndBattle = false;
        PlayerController.Instance.StopAllMovement();
        canSwitchTurn = true;
        var player = playerCombatant.gameObject;
        var enemy = enemyCombatant.gameObject;
        playerPosBeforeCombat = player.transform.position;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        enemy.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Camera.main.transform.position = new Vector3(0, 0, -10);
        UIManager.Instance.OnBattleSceneLoaded();
        player.transform.position = new Vector3(UIManager.Instance.playerSpot.transform.position.x, UIManager.Instance.playerSpot.transform.position.y, 0);
        enemy.transform.position = new Vector3(UIManager.Instance.enemySpot.transform.position.x, UIManager.Instance.enemySpot.transform.position.y + 0.5f, 0);

    }

    public void ResetExplorationScene()
    {
        if (playerCombatant == null) return;
        var player = playerCombatant.gameObject;

        // reset enemy as well when lose
        if (battleState== BattleState.Lost)
        {
            var enemy = enemyCombatant.gameObject;
            enemy.transform.localScale = Vector3.one;
            enemy.GetComponent<EnemyInteractable>().currentInteractState = Interactable.InteractState.CanInteract;
        }
        player.transform.position = playerPosBeforeCombat;
        player.transform.localScale = Vector3.one;
        playerCombatant.StopAllCoroutines();
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
