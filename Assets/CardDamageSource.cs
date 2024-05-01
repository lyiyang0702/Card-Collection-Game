using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDamageSource : MonoBehaviour
{
    public Damageable owner;
    public int baseDamage;
    public int damage;  
    public CardScriptableObject cardInfo;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCard(CardScriptableObject cardInfo, PlayerCombatantController owner)
    {
        this.cardInfo = cardInfo;
        this.owner = owner;
        baseDamage = cardInfo.attack;
        damage = baseDamage;
    }
}
