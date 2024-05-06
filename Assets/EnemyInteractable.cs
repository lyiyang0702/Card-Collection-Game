using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteractable : Interactable
{
    public override void InteractAction()
    {
        base.InteractAction();
        gameObject.transform.SetParent(CombatManager.Instance.transform);
        CombatManager.Instance.enemyCombatant = gameObject.GetComponent<EnemyCombatantController>();
        SceneManager.LoadScene("BattleScene",LoadSceneMode.Additive);

    }


}
