using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDamageSource : MonoBehaviour
{
    public PlayerCombatantController owner;
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
        baseDamage = (int)cardInfo.colorTier;
        damage = baseDamage;
    }

    public void DestroyCard()
    {
        PlayerController.Instance.inventory.RemoveCard(cardInfo);
        Destroy(gameObject);
    }

}
