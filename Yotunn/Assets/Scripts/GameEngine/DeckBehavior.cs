using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehavior : MonoBehaviour {

     List<CarteRessource> _MainDeck = new List<CarteRessource>();
    Stack<CardManager> _CardsInDeck = new Stack<CardManager>();

    public Stack<CardManager> CardsinDeck
    {
        get { return _CardsInDeck; }
        set { _CardsInDeck = value; }
    }




    public void Start()
    {
        foreach(CarteRessource card in _MainDeck)
        {
            CardManager newCard = (CardManager)Object.Instantiate(GlobalSettings.instance.cardPrefab);
            newCard.cardAsset = card;
            _CardsInDeck.Push(newCard);
        }
    }



    public CardManager Draw()
    {
        return _CardsInDeck.Pop();
    }

    public CardManager[] DrawMultiple(int num)
    {
        CardManager[] drawnCards = new CardManager[num];
        for (int i = 0; i < num; i++)
        {
            drawnCards[i] = _CardsInDeck.Pop();
        }

        return drawnCards;
    }
}
