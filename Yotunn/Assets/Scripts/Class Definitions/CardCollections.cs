using System;
using System.Collections.Generic;

namespace Cards
{
    internal class Hand
    {

        private List<Carte> _lsCartes;
        public List<Carte> lsCartes
        {
            get { return _lsCartes; }
        }

        public Hand(List<Carte> pLsCartes)
        {
            _lsCartes = new List<Carte>(pLsCartes);
        }
        public Hand()
        {
            _lsCartes = new List<Carte>();
        }

        //Surcharge de l'opérateur +. Avec ceci, Hand + Carte et Hand += Carte retournent la main avec la carte ajoutée.
        public static Hand operator +(Hand hand, Carte newCard)
        {
            hand._lsCartes.Add(newCard);

            return hand;
        }

        public Carte SelectCard()
        {
            //TODO implement actual selection mechanics.
            throw new NotImplementedException();
        }
    }

    class Deck
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