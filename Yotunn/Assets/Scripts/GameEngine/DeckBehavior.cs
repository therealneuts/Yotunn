using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckBehavior : MonoBehaviour {

    public DeckAsset decklist;
     List<CarteRessource> _MainDeck = new List<CarteRessource>();
     Stack<CardManager> _CardsInDeck = new Stack<CardManager>();
      

    public Stack<CardManager> CardsinDeck
    {
        get { return _CardsInDeck; }
        set { _CardsInDeck = value; }
    }



    
    public void Start()
    {
        
       
        _MainDeck.AddRange(decklist.Cards);
        
        foreach(CarteRessource card in _MainDeck)
        {
            
            CardManager newCard = Object.Instantiate(GlobalSettings.instance.cardPrefab, parent: transform, position: (transform.position + new Vector3(0f, 0f, .5f)), rotation: Quaternion.Euler(0, 180, 0));
            newCard.InitializeFromCardAsset(card);

            newCard.gameObject.SetActive(false);

            _CardsInDeck.Push(newCard);
            
        }

        ShuffleDeck();
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

    //Brasse le deck en générant une valeur aléatoire entre 0 et 1 pour chaque carte, puis ordonnant les cartes par cette valeur.
    public void ShuffleDeck()
    {
        var query = CardsinDeck.OrderBy(c => Random.value);

        Stack<CardManager> shuffledDeck = new Stack<CardManager>();

        foreach(CardManager card in query)
        {
            shuffledDeck.Push(card);
        }

        CardsinDeck = shuffledDeck;
    }
}
