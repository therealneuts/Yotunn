using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cards;

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

public enum CardTypes
{
    Avatar,
    Creature,
    Skill,
    Boost,
    Status
}

public class CarteRessource : ScriptableObject
{
    //Cet  objet dérive de ScriptableObject, lequel dérive de la classe object, cela me permet de ne pas toujours 
    //recréer le code pour un objet voulu

    // l'objet contiendra toutes les informations de la majorité des cartes
    [Header("General info")]
    public CharacterAsset RessourceJoueurCharacter;  // if this is null, it`s a neutral card, ne fait aucun dommage
    [TextArea(2, 3)]
    public string Description;  // Description for spell or character
    public Sprite ImageRessource; //Prend l'image de la carte voulu
    public int CoutMana;
    public CardTypes type;
    public string cardScript;

    [Header("Creature Info")]
    public int MaxHealth;
    public int Power;
    public int AttacksForOneTurn = 1;

    [Header("Spell Info")]
    public TargetingOptions Targets;

}
