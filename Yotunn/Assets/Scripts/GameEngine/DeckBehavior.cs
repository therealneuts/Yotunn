using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehavior : MonoBehaviour {

      List<CarteRessource> _MainDeck = new List<CarteRessource>();
    Stack<CardManager> CardsInDeck = new Stack<CardManager>();

    
     
    
    public void GenerateDeck()
    {
        foreach(CarteRessource card in _MainDeck)
        {
            CardManager newCard = (CardManager)Object.Instantiate(GlobalSettings.instance.cardPrefab);
            newCard.cardAsset = card;
            CardsInDeck.Push(newCard);
        }
    }


    public CardManager Draw()
    {
        return CardsInDeck.Pop();
    }

    public CardManager[] DrawMultiple(int num)
    {
        CardManager[] drawnCards = new CardManager[num];
        for (int i = 0; i < num; i++)
        {
            drawnCards[i] = CardsInDeck.Pop();
        }

        return drawnCards;
    }
}
