using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteractable : Interactable
{
    public override void InteractAction()
    {
        base.InteractAction();
        DontDestroyOnLoad(gameObject);
        CombatManager.Instance.enemy = gameObject;
        SceneManager.LoadScene("BattleScene");

        
    }


}
