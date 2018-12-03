using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReserveMana : MonoBehaviour {


    [Header("Référenciez le nombre de mana default")]
    public int DefaultNombrePourTour;
    [Header("Pour les tests seulements")]
    public int inTestShard;
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
                //si la valeur n'est pas plus grande que 10 et pas plus petite que 0
                _inNombrePourTour = value;

        }
    }

    //Besoin d'un int du nombre totale de mana libre ou NON UTILISÉ
    private int _inNombreShardNonUtilise;
    public int NombreShardDispo
    {
        get { return _inNombreShardNonUtilise; }
        set
        {
            //Si le nombre _inNombreShardNonUtilise est plus petit que 1 de setter à zero
            //afin de ne pas a avoir à gérer avec les exceptions
            if (value < 1)
            {
                //Zéro
                _inNombreShardNonUtilise = 0;
                //FAire certain que toutes les images affichés
                //Son d'une transparence gray
                for (int i = 0; i < _inNombrePourTour; i++)
                {
                    lsImageShards[i].color = Color.gray;
                }
            }
            //Si la valeur est plus grande que le nombre que le tour permet
            //Setter la valeur pour le nombre que le tour permet
            else if (value > _inNombrePourTour)
            {
                _inNombreShardNonUtilise = _inNombrePourTour;

                for (int i = 0; i < _inNombrePourTour; i++)
                {
                    lsImageShards[i].color = Color.white;
                }
            }
            //si la valeur est plus grande que le nombre _inNombreShardNonUtilise
            //nous ajoutons des shards on doit faire certain que les shard image soit visible
            else if (value > _inNombreShardNonUtilise)
            {
                //Changer la valeur desShardsNonUtilisé pour la valeur
                _inNombreShardNonUtilise = value;

                //partant de l'index 0 j'usqu'au nombreNonUtilisé
                for(int i = 0; i < _inNombreShardNonUtilise; i++)
                {
                    lsImageShards[i].color = Color.white;
                }
            }//Sinon si la valeur est plus petite on enlève une shard
            else if(value < _inNombreShardNonUtilise)
            {
                _inNombreShardNonUtilise = value;

                //Partant de l'index des shards non utilisés jusqu'au max du tour donné
                for (int i = _inNombreShardNonUtilise; i < _inNombrePourTour; i++)
                {
                    lsImageShards[i].color = Color.grey;
                }
            }

            //Changer le text après avoir fait les correction
            txtReserveShard.text = string.Format("{0}/{1}", _inNombreShardNonUtilise, _inNombrePourTour);

            ////trouve le nombre de ShardImage qui sont invisibles 
            //int indexDebut = lsImageShards.FindAll(ShardImage => ShardImage.color == Color.clear).Count - 1;


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

        //Pour les tests
        if (Application.isEditor)
        {
            inTestShard = DefaultNombrePourTour;
        }
    }

    private void FixedUpdate()
    {
        //Pour les tests
        if (Application.isEditor)
        {
            NombreShardDispo = inTestShard;
        }
    }

    //cette méthode sera appellé au début du jeu et à chaque tour 
    public void InitialisationShards()
    {
        //Si ma liste n'est pas null --> donc avec des images pour les images
        if (lsImageShards != null)
        {

            //à l'initialisation le nombreDisponible est égale au nombre donné pour le tour
            _inNombreShardNonUtilise = _inNombrePourTour;

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

            txtReserveShard.text = string.Format("{0}/{1}", _inNombreShardNonUtilise, _inNombrePourTour);
        }

    }
}
