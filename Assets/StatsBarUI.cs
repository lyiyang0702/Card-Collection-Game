using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsBarUI:MonoBehaviour
{
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI Hp;
    // Start is called before the first frame update
    public Damageable owner;

    private void Start()
    {
        
    }
    public void UpdateStatsBar(Damageable combatant)
    {
        owner = combatant;
        owner.OnHealthUpdatedEvent.AddListener(UpdateHealthBar);
        displayName .text = owner.displayName;
        Hp.text = "HP: " + Mathf.FloorToInt(owner.healthPoints) + "/" + owner.baseHealthPoints.ToString();
    }

    public void UpdateHealthBar(float health)
    {
        Debug.Log("Update Health");
        Hp.text = "HP: " + Mathf.FloorToInt(health) + "/" + owner.baseHealthPoints.ToString();
        
    }

}
