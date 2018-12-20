using System;
using System.Collections.Generic;

namespace Cards
{
  

    public class Deck
    {
        Stack<Carte> Cartes = new Stack<Carte>(); //Crée une pile de carte

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