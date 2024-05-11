using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : UnitySingleton<UIManager>
{
    public List<GameObject> allLevelMaps = new List<GameObject>();
    public QuipBannerController quipBannerController;
    public GameObject cameraCanvas;
    public GameObject levelMap;
    public GameObject BattleHUD;
    public GameObject inventoryUI;
    public RewardPanelController rewardPanelUI;
    public GameObject selectedCardParent;
    public List<CardScriptableObject> tempSelectedCards = new List<CardScriptableObject>();
    public GameObject playerStatsBar;
    public GameObject enemyStatsBar;
    public Button attackButton;
    public GameObject playerSpot;
    public GameObject enemySpot;
    public bool canSelectCards = false;
    public GameObject CardUIPrefab;
    public Button inventoryButton;
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

    public void ToggleAreaOneLevelMap(bool state)
    {
        levelMap.SetActive(state);
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
            inventoryButton.interactable = true;
           
        }
        else
        {
            selectedCardParent.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ClearCardChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public GameObject CreateCardUI(CardScriptableObject cardInfo)
    {
        GameObject cardObj = Instantiate(CardUIPrefab);
        cardObj.GetComponent<CardUI>().UpdateCardUI(cardInfo);
        cardObj.GetComponent<CardDamageSource>().InitializeCard(cardInfo, PlayerController.Instance.playerCombatant);

        return cardObj;
    }

    public void PopulateCardsToTransform(List<CardScriptableObject> cardList, Transform targetParent)
    {
        foreach (var card in cardList)
        {
            CreateCardUI(card).transform.SetParent(targetParent);
        }
    }
}
