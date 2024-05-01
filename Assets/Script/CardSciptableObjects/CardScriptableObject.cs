using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementalType
{
    Steel,
    Titanium,
    Plastic,
    Gold
}
[CreateAssetMenu(menuName = "Card")]

public class CardScriptableObject: ScriptableObject
{
    public string id;
    public string displayName;


    public enum ColorTier
    {
        Green,
        Blue,
        Purple
    }

    public ElementalType elementalType;
    public ColorTier colorTier;

    public int attack = 0;

    public Sprite cardSprite;
}
