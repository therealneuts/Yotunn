using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Cards;

namespace Cards
{
    public static class Battlefield
    {
        internal static Duel Players;

        /// <summary>
        /// Retourne une liste de toutes les cartes en jeu.
        /// </summary>
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

        /// <summary>
        /// Retourne une liste de toutes les cartes d'un type demandé.
        /// </summary>
        /// <typeparam name="T">Type dérivé de carte ou interface implémentée</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne une liste de toutes les cartes en jeu répondant à un prédicat.
        /// </summary>
        /// <param name="predicate">Méthode qui prend une carte comme paramètre et retourne une bool</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne les cartes jouables dans la main du joueur courant.
        /// </summary>
        /// <returns></returns>
        public static List<CardManager> GetPlayableCards()
        {
            List<CardManager> result = new List<CardManager>();

            //Recherche la main et l'énergie du joueur courant
            HandLayout currentPlayerHand = GameController.instance.CurrentPlayer.GetComponentInChildren<HandLayout>();
            int currentPlayerMana = GameController.instance.CurrentPlayer.AvailableMana;

            //Crée la liste des cartes dans la main du joueur courant.
            List<CardManager> cardsInCurrentPlayerHand = new List<CardManager>(currentPlayerHand.GetComponentsInChildren<CardManager>());

            result = cardsInCurrentPlayerHand.Where(c => (c is IPlayable && c.ManaCost <= currentPlayerMana)).ToList();

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