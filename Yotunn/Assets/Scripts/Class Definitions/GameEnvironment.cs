using System;
using System.Collections.Generic;
using Cards;

namespace Cards
{
    public static class Battlefield
    {
        internal static Duel Players;

        internal static Hand lstPermanents;

        internal static Hand Permanents { get { return lstPermanents; } }

        internal static Hand GetPermanentsOfType<T>()
        {
            Hand result = new Hand();

            foreach (Carte carte in lstPermanents.lsCartes)
            {
                if (carte is T)
                {
                    result += carte;
                }
            }
            return result;
        }

        internal static Hand GetPermanentsWhere(Func<Carte, bool> predicate)
        {
            Hand result = new Hand();


            foreach (Carte carte in lstPermanents.lsCartes)
            {
                if (predicate(carte) == true)
                {
                    result += carte;
                }
            }
            return result;
        }

        internal static Hand FindCards<T>(Hand location)
        {
            Hand result = new Hand();


            foreach (Carte carte in location.lsCartes)
            {
                if (carte is T)
                {
                    result += carte;
                }
            }

            return result;
        }
    }


    public class Duel
    {
        Player _player1;
        Player _player2;

        public Player Player1 { get { return _player1; } }
        public Player Player2 { get { return _player2; } }

        public Player GetOtherPlayer(Player first)
        {
            if (first == Player1) { return Player2; }
            else if (first == Player2) { return Player1; }
            else
            {
                throw new System.Exception("Player calling GetEnemy() is not in the game");
            }
        }

        public Duel(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
    }
}