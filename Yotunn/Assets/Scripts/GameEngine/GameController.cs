using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using DG.Tweening;
using System.Threading; //implimentation de threading
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {
    public Player CadreJoueur1;
    public Player CadreJoueur2;

    Duel Joueurs;

    private Player m_CurrentPlayer;
    public Player CurrentPlayer
    {
        get
        {
           
            return m_CurrentPlayer;
        }

        set
        {
            //Cadre l'icone joueur
            m_CurrentPlayer = value;
            //Shuffle les deck


            //Appelle toute les UpKeep des cartes du current Player
            Upkeep();
        }
    }


    public static GameController instance;

    internal delegate void StartTurnAction(Player CurrentPlayer);
    internal delegate void EndStepAction(Player CurrentPlayer);

    internal event StartTurnAction StartTurn;
    internal event EndStepAction EndStep;

    public void OnGameStart()
    {
        //Reset
        //Choisi un joueur au hasard du Duel Joueurs


        //For each Joueurs in Joueurs{
            //Le joueur choisi son avatar OU chaque joueur est assigné un avatar
            //Les avatars et les decks sont généré
    //}

    }
    

    public void Upkeep()
    {
                    
        List<CardManager> cards = Battlefield.GetPermanentsWhere((Carte c) => { return c is IHasStartTurnAction;}); //Une main est retourné 

        foreach (CardManager card in cards)
        {
            StartTurn += (card as IHasStartTurnAction).OnStartTurn;
        }
        if (StartTurn != null)
            StartTurn(CurrentPlayer);


        StartTurn = null;
       DeckBehavior deck = GetComponent<DeckBehavior>();
       // deck.Draw();
        //CurrentPlayer pige X Cartes
    }

    
    /// <summary>
    /// La méthode est appelé lorsqu'un joueur presse sur le boutton end turn
    /// </summary>
    public void EndTurn()
    {
        List<CardManager> cards = Battlefield.GetPermanentsWhere((Carte c) => { return (c is IHasEndStepAction); });

        foreach (CardManager card in cards)
        {
            StartTurn += (card as IHasEndStepAction).OnEndStep;
        }

        //Perdre cartes en mains
        //Change le Current player avec CurrentPlayer.GetEnemy
        CurrentPlayer = CurrentPlayer.Enemy;
    }
    

	// Use this for initialization
	void Awake () {
        Joueurs = new Duel(GlobalSettings.instance.player1, GlobalSettings.instance.player2);
        Battlefield.Players = Joueurs;
        CurrentPlayer = GlobalSettings.instance.player1;
        instance = this;
    }
}


