using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GraveyardBehavior : MonoBehaviour {
    Vector3 GYPosition;
    List<CardManager> CardsinGraveyard = new List<CardManager>();
    int GraveyardSize;

    private void Awake()
    {
        
    }

    public void AddCardToGraveyard(CardManager card)
    {
        
            CardsinGraveyard.Add(card);


        card.transform.DOMove(this.transform.position, 1);
        card.transform.DORotate(Vector3.zero, 0.5f);
       
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
