using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementalType
{
    Steel,
    Titanium,
    Rubber,
    Gold
}
[CreateAssetMenu(menuName = "Card")]

public class CardScriptableObject: ScriptableObject
{
    public string id;
    public string displayName;


    public enum ColorTier
    {
        Green = 1,
        Blue = 2,
        Purple = 3,
        Golden = 4
    }

    public ElementalType elementalType;
    public ColorTier colorTier;

    //public int attack = 0;

    public Sprite cardSprite;
}
