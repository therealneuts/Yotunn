using System;
using System.Collections.Generic;
using Cards;

namespace Cards
{
    internal static class Battlefield
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

    class Player
    {
        string m_Name { get; set; }
        Hand m_Hand { get; set; }

        internal Avatar avatar;

        private Deck _deck;

        public Deck Deck
        {
            get { return _deck; }
        }

        public Player Enemy { get { return Battlefield.Players.GetOtherPlayer(this); } }

        public delegate void CardPlayedHandler(Player source, IPlayable carte);

        public event CardPlayedHandler CardPlayed;

        public Player(string Name, Hand Main, Deck deck)
        {
            m_Name = Name;
            m_Hand = Main;
            _deck = deck;
        }

        public Player(String Name, Deck Librairie)
        {
            m_Name = Name;
            _deck = Librairie;
        }

        public void Draw(int Pige)
        {

        }

        public void Play(IPlayable card)
        {
            card.Play();
            if (CardPlayed != null)
            {
                CardPlayed(this, card);
            }
        }
    }
    internal class Duel
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