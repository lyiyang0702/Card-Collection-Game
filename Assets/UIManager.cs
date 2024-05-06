using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : UnitySingleton<UIManager>
{
    public GameObject cameraCanvas;
    public GameObject levelMap;
    public GameObject BattleHUD;
    public GameObject inventoryUI;
    public GameObject selectedCardParent;
    public List<CardScriptableObject> tempSelectedCards = new List<CardScriptableObject>();
    public GameObject playerStatsBar;
    public GameObject enemyStatsBar;
    public Button attackButton;
    public GameObject playerSpot;
    public GameObject enemySpot;
    public bool canSelectCards = false;
    // Start is called before the first frame update

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBattleSceneLoaded()
    {
        PlayerController.Instance.input.SwitchCurrentActionMap("UI");
        cameraCanvas.SetActive(true);
        levelMap.SetActive(false);
        enemyStatsBar.GetComponent<StatsBarUI>().UpdateStatsBar(CombatManager.Instance.enemyCombatant);
        playerStatsBar.GetComponent<StatsBarUI>().UpdateStatsBar(CombatManager.Instance.playerCombatant);
        UpdateBattleUI(CombatManager.Instance.battleState);
    }

    public void OnBattleSceneUnLoaded()
    {
        PlayerController.Instance.input.SwitchCurrentActionMap("Player");
        //temp fix
        for(int i = 0; i < selectedCardParent.transform.childCount; i++)
        {
            Destroy(selectedCardParent.transform.GetChild(i).gameObject);
        }
        cameraCanvas.SetActive(false);
        levelMap.SetActive(true);
    }

    public void UpdateBattleUI(BattleState state)
    {
        if (state == BattleState.PlayerTurn)
        {
            selectedCardParent.transform.parent.gameObject.SetActive(true);
            
        }
        else
        {
            selectedCardParent.transform.parent.gameObject.SetActive(false);
        }
    }

}
