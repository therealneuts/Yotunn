using System;
using System.Collections.Generic;
using UnityEngine;
using Cards;

namespace Cards
{
    public static class Battlefield
    {
        internal static Duel Players;

        internal static List<CardManager> lstPermanents
        {
            get
            {
                BattlegroundLayout[] playerAreas = UnityEngine.Object.FindObjectsOfType<BattlegroundLayout>();
                List<CardManager> permanents = new List<CardManager>();
                foreach (BattlegroundLayout playerArea in playerAreas)
                {
                    CardManager[] cards = playerArea.GetComponentsInChildren<CardManager>();
                    permanents.AddRange(cards);
                }

                return permanents;
            }
        }

        internal static List<CardManager> GetPermanentsOfType<T>()
        {
            List<CardManager> result = new List<CardManager>();

            foreach (CardManager carte in lstPermanents)
            {
                if (carte.CardScript is T)
                {
                    result.Add(carte);
                }
            }
            return result;
        }

        internal static List<CardManager> GetPermanentsWhere(Func<Carte, bool> predicate)
        {
            List<CardManager> result = new List<CardManager>();
            
            foreach (CardManager carte in lstPermanents)
            {
                if (predicate(carte.CardScript) == true)
                {
                    result.Add(carte);
                }
            }
            return result;
        }

        public static List<CardManager> GetAllPlayableCard()
        {
            List<CardManager> result = new List<CardManager>();
            //To do mettre toute les CardManager qui appartient au joueur Current
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
                throw new System.Exception("Player calling GetOtherPlayer() is not in the game");
            }
        }

        public Duel(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
    }
}