using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    List<CardManager> CardsinGraveyard = new List<CardManager>();
    int GraveyardSize;


    public void AddCardToGraveyard(CardManager card)
    {
        CardsinGraveyard.Add(card);
    }

    public void ShuffleinDeck()
    {
        DeckBehavior deck = GetComponentInParent<DeckBehavior>();
        foreach (CardManager carte in CardsinGraveyard)
        {
            deck.CardsinDeck.Push(carte);
            CardsinGraveyard.Remove(carte);
            GraveyardSize = GraveyardSize - 1;
        }
    }
}
