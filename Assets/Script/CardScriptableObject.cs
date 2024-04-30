using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardScriptableObject: ScriptableObject
{
    public string id;
    public string displayName;
    public enum ElementalType { 
        Steel = 0, 
        Titanium = 1, 
        Plastic, 
        Gold }


}
