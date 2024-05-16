using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
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
    public Button inventoryButtonOnMap;
    public TextMeshProUGUI enemyDefText;
    public TextMeshProUGUI enemyAtkText;
    public AudioSource soundtrack;
    public GameObject endingPanel;
    public TextMeshProUGUI battleIntroText;
    public GameObject battleIntroPanel;

    // Start is called before the first frame update

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        //DontDestroyOnLoad(gameObject);


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
        //PlayerController.Instance.input.SwitchCurrentActionMap("UI");
        inventoryButtonOnMap.gameObject.SetActive(false);
        cameraCanvas.SetActive(true);
        levelMap.SetActive(false);
        enemyStatsBar.GetComponent<StatsBarUI>().UpdateStatsBar(CombatManager.Instance.enemyCombatant);
        playerStatsBar.GetComponent<StatsBarUI>().UpdateStatsBar(CombatManager.Instance.playerCombatant);
        UpdateBattleUI(CombatManager.Instance.battleState);
        soundtrack.Stop();
    }

    public void OnBattleSceneUnLoaded()
    {
        //PlayerController.Instance.input.SwitchCurrentActionMap("Player");
        inventoryButtonOnMap.gameObject.SetActive(true);
        //temp fix
        for (int i = 0; i < selectedCardParent.transform.childCount; i++)
        {
            Destroy(selectedCardParent.transform.GetChild(i).gameObject);
        }
        cameraCanvas.SetActive(false);
        rewardPanelUI.gameObject.SetActive(false);
        levelMap.SetActive(true);
        soundtrack.Play();
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

    public GameObject CreateCardUI(CardScriptableObject cardInfo, bool shouldGlow = false)
    {
        GameObject cardObj = Instantiate(CardUIPrefab);
        var cardUI = cardObj.GetComponent<CardUI>();
        cardUI.UpdateCardUI(cardInfo,shouldGlow);
 
        cardObj.GetComponent<CardDamageSource>().InitializeCard(cardInfo, PlayerController.Instance.playerCombatant);

        return cardObj;
    }

    public void PopulateCardsToTransform(List<CardScriptableObject> cardList, Transform targetParent, bool shouldGlow = false)
    {
        if (cardList.Count == 0) return;
        foreach (var card in cardList)
        {
            CreateCardUI(card,shouldGlow).transform.SetParent(targetParent);
        }
    }

    public void ShowBattleIntroText(string message)
    {
        battleIntroText.text = message;
        battleIntroPanel.SetActive(true);
        Time.timeScale = 0;
        DisableAllInteractiveElements();
    }
    public void HideBattleIntroText() 
    {
        battleIntroPanel.SetActive(false);
        Time.timeScale = 1;
        EnableAllInteractiveElements();
    }
    public void DisableAllInteractiveElements()
    {
        foreach (var button in FindObjectsOfType<Button>())
        {
            button.interactable = false;
        }
    }
    public void EnableAllInteractiveElements()
    {
        foreach (var button in FindObjectsOfType<Button>())
        {
            button.interactable = true;
        }
    }


    //Mak new Code
    /*public void ShowTutorialBattleIntroText(string message)
    {
        TutorialBattleText.text = message;
        TutorialBattleText.gameObject.SetActive(true);
    }

    public void HideTutorialBattleIntroText() 
    {
        TutorialBattleText.gameObject.SetActive(false);
    }*/
}
