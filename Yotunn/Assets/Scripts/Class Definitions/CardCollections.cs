using System;
using System.Collections.Generic;

namespace Cards
{
    //public class List<CardManager>
    //{

    //    private List<Carte> _lsCartes;
    //    public List<Carte> lsCartes
    //    {
    //        get { return _lsCartes; }
    //    }

    //    public List<CardManager>(List<Carte> pLsCartes)
    //    {
    //        _lsCartes = new List<Carte>(pLsCartes);
    //    }
    //    public List<CardManager>()
    //    {
    //        _lsCartes = new List<Carte>();
    //    }

    //    //Surcharge de l'opérateur +. Avec ceci, List<CardManager> + Carte et List<CardManager> += Carte retournent la main avec la carte ajoutée.
    //    public static List<CardManager> operator +(List<CardManager> hand, Carte newCard)
    //    {
    //        hand._lsCartes.Add(newCard);

    //        return hand;
    //    }

    //    public Carte SelectCard()
    //    {
    //        //TODO implement actual selection mechanics.
    //        throw new NotImplementedException();
    //    }
    //}

    public class Deck
    {
        Stack<Carte> Cartes = new Stack<Carte>();

        public Deck()
        {

        }

        internal void ShuffleInto(Carte carte)
        {
            Cartes.Push(carte);
            Shuffle();
        }

        void Shuffle()
        {
            //todo implement shuffle
        }
    }
}