using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReserveMana : MonoBehaviour {



    [Header("Référenciez le nombre de mana default")]
    public int DefaultNombrePourTour;    
    [Header("Référenciez toutes les images des shards")]
    //List avec les images des mana shards des instances de la classe UI
    public List<Image> lsImageShards;
    //Référence pour le accèder au texte du ManaPool
    [Header("Référenciez le texte pour la reserve de shard")]
    public Text txtReserveShard;

    //Besoin d'un int du nombre totale de mana pour le tour
    //Définir les propriétés (get,set) pour le nombre totale de mana par tour 
    private int _inNombrePourTour;
    public int NombrePourTour
    {
        get { return _inNombrePourTour; }
        set
        {
            //Si on essait de de changer la valeur du nombre pour le tour
            //Vérifier si la valeur n'est pas plus haut que 10 ou plus petit que zer0
            //et faire correction
            if (value > 10)
                _inNombrePourTour = 10;
            else if (value < 0)
                _inNombrePourTour = 0;
            else
                _inNombrePourTour = value;
        }
    }

    //Besoin d'un int du nombre totale de mana libre ou non UTILISÉ
    private int _inNombreShardDispo;
    public int NombreShardDispo
    {
        get { return _inNombreShardDispo; }
        set
        {
            //Si le nombre _inNombreShardDispo est plus petit que 1 de setter à zero
            if (_inNombreShardDispo < 1)
                _inNombreShardDispo = 0;

            //Changer le text après avoir fait les correction
            txtReserveShard.text = string.Format("{0}/{1}", _inNombreShardDispo, _inNombrePourTour);
        }
    }


    private void Awake()
    {
        //Set la propriété par default
        NombrePourTour = DefaultNombrePourTour;
    }

    private void Start()
    {
        //Initialise les shards qui vont être vue dans le jeux
        InitialisationShards();
    }

    //cette méthode sera appellé au début du jeu et à chaque tour 
    public void InitialisationShards()
    {
        //Si ma liste n'est pas null --> donc avec des images pour les images
        if (lsImageShards != null)
        {
            //à l'initialisation le nombreDisponible est égale au nombre donné pour le tour
            _inNombreShardDispo = _inNombrePourTour;

            //pour que i ne soit pas pa égal ou plus grand que la grandeur de la liste
            for (int i = 0; i < lsImageShards.Count; i++)
            {
                //Setter le paramètre de couleur à complètement transparent soit en RGBA(0,0,0,0)
                //Avec Color de UnityEngine
                lsImageShards[i].color = Color.clear;
            }

            //pour que i ne soit pas égal ou plus grand que le nombre total pour le tour 
            for (int i = 0; i < _inNombrePourTour; i++)
            {
                //rendre L'image de la shard visible en changeant sa couleur soit en RGBA(1,1,1,1)
                //Color.white
                lsImageShards[i].color = Color.white;
            }

            txtReserveShard.text = string.Format("{0}/{1}", _inNombreShardDispo, _inNombrePourTour);
        }

    }
}
