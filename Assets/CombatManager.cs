using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;

public enum BattleState {None, Start, PlayerTurn, EnemyTurn, Won, Lost}
public class CombatManager : UnitySingleton<CombatManager>
{
    public GameObject playerPrefab;
    public GameObject enemy;
    public BattleState battleState = BattleState.None;
    public GameObject playerSpot;
    public GameObject enemySpot;
    
    // Start is called before the first frame update
    override public void Awake()
    {
        base.Awake();
        playerPrefab = PlayerController.Instance.gameObject;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnBattleSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCombatScene()
    {
        playerPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        enemy.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        playerPrefab.GetComponent<PlayerController>().cam.transform.position = new Vector3(0,0,-10);
        battleState = BattleState.Start;
        PlayerController.Instance.input.SwitchCurrentActionMap("UI");
        UIManager.Instance.cameraCanvas.SetActive(true);
        UIManager.Instance.BattleHUD.SetActive(true);
        playerPrefab.transform.position = new Vector3(playerSpot.transform.position.x, playerSpot.transform.position.y + 0.5f, 0);
        enemy.transform.position = new Vector3(enemySpot.transform.position.x, enemySpot.transform.position.y + 0.5f, 0);

    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            InitializeCombatScene();
            DecideTurn();
            if (battleState == BattleState.PlayerTurn)
            {
                UIManager.Instance.selectedCardParent.SetActive(true);
            }
        }
    }

    private void DecideTurn()
    {
        PlayerCombatantController player = playerPrefab.GetComponent<PlayerCombatantController>();
        EnemyCombatantController enemy = this.enemy.GetComponent<EnemyCombatantController>();

        if (enemy.elementalType == ElementalType.Steel || enemy.elementalType == ElementalType.Plastic)
        {
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            battleState = BattleState.PlayerTurn;
        }
    }
}
