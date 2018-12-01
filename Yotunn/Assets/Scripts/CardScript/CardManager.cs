using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Cards;

// Détient toutes les références du textes et des Images sur la carte
// Elle informe au GameObject quelle information qu'elle doit prendre
public class CardManager : MonoBehaviour {

    //Ici nous avons besoin d'une référence vers la CarteRessource du jeu pour contrôler les différentes propriétés
    //de l'objet dans le plan du jeu, soit le text les image, et les différentes propriétés.
    public CarteRessource CarteRessource;
    //Étant donné que'une carte transporte un preview, une copie conforme à la carte mais qui contient
    //seulement les propriétés graphiques de la carte donnée, le CardManager doit pouvoir connaître celui
    //du preview, lequel transporte lui-même un CardManager.
    public CardManager CartePreview;
    
    [Header("Image References")]
    //Tous les éléments graphiques d'une carte
    public Image ImageRubanHaut;
    public Image ImageRubanBas;
    public Image ImageCarteGraphique;
    public Image ImageCorps;
    public Image ImageDevantFrame;

    //Éléments graphique qui sera vu lorsque nous activons, afin que nous puissons informer au joueur qu'il peut
    //faire une action avec celle-ci
    public SpriteRenderer ImageGlowDevant;
    public Image ImageGlowDerriere;

    Carte _cardScript;

    public Carte CardScript
    {
        get
        {
            return _cardScript;
        }

        set
        {
            _cardScript = value;
        }
    }
    //Références au propriétaire de la carte et à son avatar.
    Player _owner;
    internal Player Owner
    {
        get
        {
            return _owner;
        }

        set
        {
            _owner = value;
        }
    }

    public CardManager Avatar
    {
        get
        {
            return Owner.Avatar;
        }
    }


    /*
     Champs, propriétés et événements concernant les valeurs numériques des cartes. Les événements nous donnent un grand degré
     de flexibilité de design en plus d'améliorer la modularisation et la lisibilité du code. Les objets qui affichent ces
     valeurs peuvent maintenant faire leur propre travail.

        -Alex C.
    */
    public delegate void OnParameterChanged(CardManager card, int difference);

    public int _health;
    public int _power;
    public int _manaCost;

    public event OnParameterChanged HealthChanged;
    public event OnParameterChanged PowerChanged;
    public event OnParameterChanged ManaCostChanged;

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            int initial = _health;
            _health = value;
            int difference = _health - initial;
            if (HealthChanged != null)
            {
                HealthChanged(this, difference);
            }
            
            if (_health <= 0)
            {
                Discard();
            }
        }
    }

    public int Power
    {
        get
        {
            return _power;
        }

        set
        {
            int initial = _power;
            _power = value;
            int difference = _power - initial;
            if (PowerChanged != null)
            {
                PowerChanged(this, difference);
            }
        }
    }

    public int ManaCost
    {
        get
        {
            return _manaCost;
        }

        set
        {
            int initial = _manaCost;
            _manaCost = value;
            int difference = _manaCost - initial;
            if (ManaCostChanged != null)
            {
                ManaCostChanged(this, difference);
            }
        }
    }

    //La méthode Awake qui à été donné à travers l'héritage de la classe MonoBehavior qui se fait après l'instanciation
    //de tous les objets devant être instancié au début du jeu
    void Awake()
    {
        //Si une carte asset a été référenciée, et donc non null, l'instance de CarteRessourceEnGameObject sait
        //qu'il doit mettre toutes les informations dans le graphique de la carte sur le jeu
        if (CarteRessource != null)
        {
            //La méthode qui insère toute l'information de la carte ressource dans la carte instancié du jeu
            CarteRessourceToGameGraphique();
            InitializeValues();
        }
    }

    /// <summary>
    /// Un propriété qui nous laissera jouer la carte et si elle peut être jouer une couleur de fond sera affiché pour
    /// que le joueur soit notifié
    /// </summary>
    private bool canBePlayed = false;
    public bool CanBePlayedNow
    {
        get
        {
            
            return canBePlayed;
        }

        set
        {            
            canBePlayed = value;
            //La raison pour laquelle nous Hard Codons la propriété de CanBePlayedNow est parce qu'à chaque changement 
            //de celui-ci nous voulons copoier la valeur donnée à l'objet ImageGlowDevant, ce qui activera
            //notre belle image de fond pour notre carte
            ImageGlowDevant.enabled = value;
        }
    }



    public void InitializeValues()
    {
        ManaCost = CarteRessource.CoutMana;
        Type cardType = System.Type.GetType(CarteRessource.cardScript);

        if (!cardType.IsSubclassOf(typeof(Carte)))
        {
            throw new System.Exception("CarteRessource.cardScript is not a subclass of Carte");
        }

        CardScript =  (Carte)gameObject.AddComponent(cardType);
        if (CardScript is Entity)
        {
            Health = CarteRessource.MaxHealth;
            Power = CarteRessource.Power;
        }
        else if (CardScript is Skill)
        {
            Power = CarteRessource.Power;
        }
    }

    //Méthode qui met toutes les informations nécessaire à la carte
    public void CarteRessourceToGameGraphique()
    {
        //Sera fait pour toutes les cartes
        
        if (CarteRessource.RessourceJoueurCharacter != null)
        {
            //Si CharacterAsset n'est pas null cela veut dire que c'est un character, alors nous mettons de la couleur
            //lesquelles sont prédéfini selon une classe Color32 une classe d'Unity qui représente des couleurs en format 32 bit
            //et laquelles est défini par la classe de character
            //Cela change tout le look et le feel de la classe
            ImageCorps.color = CarteRessource.RessourceJoueurCharacter.ClassCardTint;
            ImageDevantFrame.color = CarteRessource.RessourceJoueurCharacter.ClassCardTint;
            ImageRubanHaut.color = CarteRessource.RessourceJoueurCharacter.ClassRibbonsTint;
            ImageRubanBas.color = CarteRessource.RessourceJoueurCharacter.ClassRibbonsTint;
        }
        else
        {
            //ImageCorps.color = GlobalSettings.Instance.CardBodyStandardColor;
            ImageDevantFrame.color = Color.white;
            //ImageRubanHaut.color = GlobalSettings.Instance.CardRibbonsStandardColor;
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;

            //Si n'est pas un character asset mettre le type de la carte
        }


        //si le CartePreview est null donc nous savons que celui-ci est un l'objet
        //de la carte et non le deuxième instancié
        if (CartePreview != null)
        {
            //Afin que nous n'ayons pas une boucle qui ne finirait jamais 
            //le preview devrait avoir un CarteRessourceEnGameObject aussi, mais celui-ci devrait être null
            CartePreview.CarteRessource = CarteRessource;
            //Appeler la  méthode readCardFromAsset() du preview
            CartePreview.CarteRessourceToGameGraphique();
        }
    }

    private void Discard()
    {
        //TODO implement discarding.
        throw new NotImplementedException();
    }
}


