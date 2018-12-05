using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class GameController : MonoBehaviour {

    Duel Joueurs;

    private Player _CurrentPlayer;
    internal Player CurrentPlayer
    {
        get
        {
            return _CurrentPlayer;
        }

        set
        {
            _CurrentPlayer = value;

           
        }
    }

    public static GameController instance;

    public void OnGameStart()
    {
        //Reset
        //Choisi un joueur au hasard du Duel Joueurs


        //For each Joueurs in Joueurs{
            //Le joueur choisi son avatar OU chaque joueur est assigné un avatar
            //Les avatars et les decks sont généré
    //}

    }

    internal void Upkeep(Player CurrentPlayer)
    {
        Hand cards = Battlefield.GetPermanentsWhere(delegate (Carte carte)
        {
            return (carte is IHasStartTurnAction);
        }); 

        foreach (Carte card in cards.lsCartes)
        {
            StartTurn += (card as IHasStartTurnAction).OnStartTurn;
        }

        //CurrentPlayer pige X Cartes
    }

    

    internal void EndTurn(Player Current)
    {
        Hand cards = Battlefield.GetPermanentsWhere(delegate (Carte carte)
        {
            return (carte is IHasEndStepAction);
        });

        foreach (Carte card in cards.lsCartes)
        {
            StartTurn += (card as IHasEndStepAction).OnEndStep;
        }

        //Perdre cartes en mains
        //Change le Current player avec CurrentPlayer.GetEnemy
        //appelle Upkeep avec le nouveau CurrentPlayer en paramètre
        //Shuffle les deck
    }

    internal delegate void StartTurnAction(Player CurrentPlayer);
    internal delegate void EndStepAction(Player CurrentPlayer);

    internal event StartTurnAction StartTurn;
    internal event EndStepAction EndStep;
    

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        Joueurs = new Duel(GlobalSettings.instance.player1, GlobalSettings.instance.player2);
        Battlefield.Players = Joueurs;
        CurrentPlayer = GlobalSettings.instance.player1;
        instance = this;
    }
}
