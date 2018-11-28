using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions
{
    NoTarget,
    AllCreatures, 
    EnemyCreatures,
    YourCreatures, 
    AllCharacters, 
    EnemyCharacters,
    YourCharacters
}

public class CardAsset : ScriptableObject
{
    //Cet  objet dérive de ScriptableObject, lequel dérive de la classe object, cela me permet de ne pas toujours 
    //recréer le code pour un objet voulu

    // l'objet contiendra toutes les informations de la majorité des cartes
    [Header("General info")]
    public CharacterAsset characterAsset;  // if this is null, it`s a neutral card, ne fait aucun dommage
    [TextArea(2, 3)]
    public string Description;  // Description for spell or character
    public Sprite CardImage; //Prend l'image de la carte voulu
    public int ManaCost;

    [Header("Creature Info")]
    public int MaxHealth;
    public int Attack;
    public int AttacksForOneTurn = 1;
    public bool Taunt;
    public bool Charge;
    public string CreatureScriptName;
    public int specialCreatureAmount;

    [Header("SpellInfo")]
    public string SpellScriptName;
    public int specialSpellAmount;
    public TargetingOptions Targets;

    [Header("TypeImage")]
    public Sprite CardType;

}
