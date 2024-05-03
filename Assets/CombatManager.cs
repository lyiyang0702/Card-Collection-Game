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
    public GameObject playerPrefab;
    public GameObject enemy;
    public BattleState battleState = BattleState.None;
    public UnityEvent SwicthTurnEevent;
    Vector3 playerPosBeforeCombat;
    // Start is called before the first frame update
    override public void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnBattleSceneLoaded;
        SceneManager.sceneUnloaded += OnBattleSceneUnloaded;
    }

  

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCombatScene()
    {
        playerPosBeforeCombat = playerPrefab.transform.position;
        playerPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        enemy.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Camera.main.transform.position = new Vector3(0,0,-10);
        UIManager.Instance.OnBattleSceneLoaded();
        playerPrefab.transform.position = new Vector3(UIManager.Instance.playerSpot.transform.position.x, UIManager.Instance.playerSpot.transform.position.y + 0.5f, 0);
        enemy.transform.position = new Vector3(UIManager.Instance.enemySpot.transform.position.x, UIManager.Instance.enemySpot.transform.position.y + 0.5f, 0);

    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            if (enemy == null) return;
            if(playerPrefab == null) return;
            DecideTurn();
            InitializeCombatScene();

        }
    }

    private void OnBattleSceneUnloaded(Scene scene)
    {
        if (scene.name == "BattleScene")
        {
            playerPrefab.transform.position = playerPosBeforeCombat;
            playerPrefab.transform.localScale = Vector3.one;
            enemy.transform.localScale = Vector3.one;
            battleState = BattleState.None;
            Destroy(enemy);
            UIManager.Instance.OnBattleSceneUnLoaded();
        }
    }
    private void DecideTurn()
    {
        PlayerCombatantController player = playerPrefab.GetComponent<PlayerCombatantController>();
        EnemyCombatantController enemy = this.enemy.GetComponent<EnemyCombatantController>();
        player.OnDeathEvent.AddListener(OnEndBattle);
        enemy.OnDeathEvent.AddListener(OnEndBattle);

        if (enemy.elementalType == ElementalType.Steel || enemy.elementalType == ElementalType.Rubber)
        {
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            battleState = BattleState.PlayerTurn;
        }

        SwicthTurnEevent.Invoke();
    }

    void OnSwitchTurn(BattleState state)
    {

    }

    void OnEndBattle(Damageable damageable)
    {
        if (damageable.isEnemy)
        {
            battleState = BattleState.Won;
            SceneManager.UnloadSceneAsync("BattleScene");
        }
        else if (damageable.isPlayer)
        {
            battleState = BattleState.Lost;
        }
    }
}
