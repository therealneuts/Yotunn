using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    List<CardManager> CardsinGraveyard = new List<CardManager>();

    public void AddCardToGraveyard(CardManager card)
    {
        CardsinGraveyard.Add(card);
    }

}
