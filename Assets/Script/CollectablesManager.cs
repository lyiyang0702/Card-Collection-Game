using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class CollectablesManager : MonoBehaviour
{
    public GameObject card;
    public GameObject enemy;
    public GameObject canvas;
    public TextMeshProUGUI TutorialBattleText;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (card == null)
        {
            ShowTextAndEnemy();
        }

        if (enemy == null || !enemy.activeInHierarchy)
        {
            canvas.SetActive(false);
        }

        if (GameManager.Instance.currentArea == 0)
        {
            if(CombatManager.Instance.battleState == BattleState.Start)
            {
                ShowTutorialBattleIntroText("Place 3 or 5 same elemental type cards to trigger the combo effect");
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    HideTutorialBattleIntroText();
                }
            }
        }

    }

    public void ShowTextAndEnemy()
    {
        canvas.SetActive(true);
        if (enemy != null)
        {
            enemy.SetActive(true);
        }
    }


    public void ShowTutorialBattleIntroText(string message)
    {
        TutorialBattleText.text = message;
        TutorialBattleText.gameObject.SetActive(true);
    }

    public void HideTutorialBattleIntroText() 
    {
        TutorialBattleText.gameObject.SetActive(false);
    }
}
