using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using DG.Tweening;
using System.Threading; //implimentation de threading
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {
    //Instance du GameController afin que nous puissons accéder à ses méthodes et ses champs en utilisant qu'une seul instance. 
    //Nous allons pointé une variable de l'objet GAmeController avec cette variable
    public static GameController instance;

    //Déclaration d'une variable de type Duel, lequel détient une référence a chacun des objets Joueurs, pour que nous puissons les atteindre plus facilement 
    Duel Joueurs;
    
    //Lors de l'inisialisation de l'objet dans le jeu cette méthode est appelée. Elle est appelée tout au départ
    void Awake()
    {
        //Set un nouvel objet de type Duel en assigant par son constructeur l'instance du joueur 1 et l'instance du joueur 2 qui se retrouve tout deux référenciés dans l'instance de GlobalSettings.
        Joueurs = new Duel(GlobalSettings.instance.player1, GlobalSettings.instance.player2);
        //set dans notre méthode static la propriété Players (Variable pour Type Duel) pour pointé vers le duel que nous venons tout juste d'instancier
        Battlefield.Players = Joueurs;
        //Ici nou settons le CurrentPlayer par le joueur 1, car nous débutons une nouvelle partie.
        m_CurrentPlayer = GlobalSettings.instance.player1;
        //Afin de garder l'objet en mémoire
        instance = this;
    }

    //Propriété qui dit à quel joueur est le tour elle pointe vers un Objet du type Joueur
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


    //Déclaration d'une méthode qui ne retounrne rien et demande en surcharge un Objet de typ Joueur
    public delegate void StartTurnAction(Player CurrentPlayer);
    public delegate void EndStepAction(Player CurrentPlayer);
    //Events du type Sart et End
    public event StartTurnAction StartTurn;
    public event EndStepAction EndStep;

    public void OnGameStart()
    {

    }

    //UpKeep est appelé à chaque fois que la propriété m_CurrentPlayer, lequel lorsqu'il change signifie le changement de tour
    public void Upkeep()
    {
        //Une liste de carteManager (des cartes) est retourné, laquelle contient seulement que des cartes contenant l'interface IHasStartTurnAction
        //laquelle dicte à ceux qui recoivent cette interface qu'ils doit avoir une méthode OnStartTurn
        List<CardManager> cards = Battlefield.GetPermanentsWhere((Carte c) => { return c is IHasStartTurnAction;});

        //Pour toute les cartes qui on été trouvé avec l'interface IHasStartTurnAction
        //si c'est le current player ajouter la méthode d'OnStartTurn à l'événement StartTurn
        foreach (CardManager card in cards)
        {
            if(card.Owner == CurrentPlayer)
                StartTurn += (card as IHasStartTurnAction).OnStartTurn;
        }

        //assigner au début de tour une méthode qui affiche à partir de l'instance du système de message le joueur à qui est le tour
        StartTurn += (Player p) => { Message.Instance.ShowTurnMessage(p); };


        //si n'est pas null faire jouer tout les méthodes mises dans le StartTurn.
        if (StartTurn != null)
            StartTurn(CurrentPlayer);

        //Nétoyer l'événement du début du tour 
        StartTurn = null;
        DeckBehavior deck = GetComponent<DeckBehavior>();
        //Fait piger une carte au joueur à qui est le tour par la propriété m_CurrentPlayer à partir d'une fonction réservé au objet du type Joueur
        CurrentPlayer.Draw(1);
        //CurrentPlayer pige X Cartes

    }

    
    /// <summary>
    /// La méthode est appelée lorsqu'un joueur presse sur le boutton end turn
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

        //set le mana qu'il a le droit pour le tour 
        CurrentPlayer.hisManaReserve.NombrePourTour = 10;

        //Set le nombre dispo égal au nombre qu'il a le droit pour le nombre d etour donné
        CurrentPlayer.hisManaReserve.NombreShardDispo = CurrentPlayer.hisManaReserve.NombrePourTour;
    }
    

}


