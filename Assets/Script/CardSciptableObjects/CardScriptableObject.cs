using System;
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

public class CardScriptableObject: ScriptableObject,IEquatable<CardScriptableObject>
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


    public bool Equals(CardScriptableObject other)
    {
        if (other == null) return false;
        return other.colorTier == colorTier && other.elementalType == elementalType;
    }
}
