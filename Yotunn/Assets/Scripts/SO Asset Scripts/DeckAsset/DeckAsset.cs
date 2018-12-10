using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Deck Asset", order = 1)]
public class DeckAsset : ScriptableObject {

    public CarteRessource[] Cards;
}
