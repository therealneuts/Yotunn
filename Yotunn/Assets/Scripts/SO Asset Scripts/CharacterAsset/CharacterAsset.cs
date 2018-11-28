using UnityEngine;
using System.Collections;

public enum CharClass{ Elf, Monk, Warrior}

public class CharacterAsset : ScriptableObject 
{
    //Contient toutes les informations nécessaires pour la construction d'un objet de style character
	public CharClass Class;
	public string ClassName;
	public int MaxHealth = 30;
	public string HeroPowerName;
    //Contient les éléments graphiques
	public Sprite AvatarImage;
    public Sprite HeroPowerIconImage;
    public Sprite AvatarBGImage;
    public Sprite HeroPowerBGImage;
    public Color32 AvatarBGTint;
    public Color32 HeroPowerBGTint;
    public Color32 ClassCardTint;
    public Color32 ClassRibbonsTint;
}
