using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Cards;

/// <summary>
/// Le CardManager est un script attaché au sommet de la carte qui prend en charge la logique des actions produites sur la carte et par la carte.
/// Avant d'être instancié, CardManager est une page vierge -- une carte sans identité. Son identité se trouve dans l'objet CarteRessource qui lui est assigné.
/// Il représente une instance d'une carte définie par un CarteRessource quelconque. Lorsqu'il est instancié avec une CarteRessource, il récupèrera son identité
/// de celle-ci, et règlera ses valeurs par défaut. C'est le processus de création d'une nouvelle carte.
/// 
/// -Alex C.
/// </summary>
public class CardManager : MonoBehaviour {

    //Ici nous avons besoin d'une référence vers la CarteRessource du jeu pour contrôler les différentes propriétés
    //de l'objet dans le plan du jeu, soit le text les image, et les différentes propriétés.
    public CarteRessource cardAsset;
    //Étant donné que'une carte transporte un preview, une copie conforme à la carte mais qui contient
    //seulement les propriétés graphiques de la carte donnée, le CardManager doit pouvoir connaître celui
    //du preview, lequel transporte lui-même un CardManager.
    public CardManager CartePreview;
    
    [Header("Image References")]
    //Tous les éléments graphiques d'une carte
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

    /// <summary>
    ///      Champs, propriétés et événements concernant les valeurs numériques des cartes. Les événements nous donnent un grand degré
    ///de flexibilité de design en plus d'améliorer la modularisation et la lisibilité du code. Les objets qui affichent ces
    ///valeurs peuvent maintenant faire leur propre travail.
    ///
    ///-Alex C.
    /// </summary>
    public delegate void OnParameterChanged(CardManager card, int difference);

    int _health;
    int _power;
    int _manaCost;

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

    /// <summary>
    /// Un propriété qui nous laissera jouer la carte et si elle peut être jouer une couleur de fond sera affiché pour
    /// que le joueur soit notifié
    /// </summary>
    public event Action<bool> CanPlayChanged;
    private bool canPlay = false;
    public bool CanPlay
    {
        get
        {
            return canPlay;
        }

        set
        {            
            canPlay = value;
            if (CanPlayChanged != null)
            {
                CanPlayChanged(value);
            }
        }
    }

    public delegate void InitializedAction();
    public event InitializedAction Initialized;

    //La méthode Awake qui à été donné à travers l'héritage de la classe MonoBehavior qui se fait après l'instanciation
    //de tous les objets devant être instanciés au début du jeu.
    void Awake()
    {
        //Si une carte asset a été référenciée, et donc non null, l'instance de CarteRessourceEnGameObject sait
        //qu'il doit mettre toutes les informations dans le graphique de la carte sur le jeu. Généralement, CarteRessource ne devrait jamais être null.
        if (cardAsset != null)
        {
            //Les méthodes qui insèrent toute l'information de la carte ressource dans la carte instanciée.
            InitializeValues();
        }
    }

    private void Start()
    {
        //Lance l'événement qui notifie les autres composantes de la carte que celle-ci a fini son initialisation.
        if (Initialized != null)
        {
            Initialized();
        }
    }

    /// <summary>
    /// Par cette méthode, le CardManager récupère son identité de l'objet CarteRessource qui lui est assigné. C'est ici que la carte est initialisée.
    /// </summary>
    public void InitializeValues()
    {
        //Nous somme malheureusement forcés d'utiliser une référence en chaîne de caractères littérale pour indiquer le script C# relié à la carte instanciée.
        //Ce bout de code à l'effet de s'assurer que le nom du script a été bien écrit (il doit être identique au nom de la classe C# associé à la carte).
        //Si le nom a êté mal écrit (ou si un script non-relié à la classe Carte est donné), le jeu lancera une exception.
        Owner = GetComponentInParent<Player>();
        //Premièrement, on récupère le type du script en utilisant la méthode .NET qui recherche un type dans l'assembly qui correspond à une chaîne de caractères.
        Type cardType = System.Type.GetType(cardAsset.cardScript);
        //Ensuite, on vérifie que ce type est un sous-type de Carte. Cette méthode donnerait faux si le type est précisément Carte, mais serait le résultat d'une
        //erreur de notre part et ne devrait pas se produire.
        if (!cardType.IsSubclassOf(typeof(Carte)))
        {
            throw new System.Exception("CarteRessource.cardScript wasn't found or is not a subclass of Carte");
        }

        ManaCost = cardAsset.CoutMana;

        //gameObject.AddComponent instancie un script, l'ajoute à l'objet, et retourne le script instancié, qui est assigné à la référence CardScript.
        //Un cast vers Carte est nécéssaire, puisqu'il retourne une référence à un Component, classe qui se trouve plusieurs niveaux d'héritage au-dessus de Carte.
        CardScript = (Carte)gameObject.AddComponent(cardType);

        //Maintenant que nous avons une référence au script, nous pouvons déterminer le type de la carte. Ceci nous permet d'ignorer les valeurs non-nécessaires.
        if (CardScript is Entity)
        {
            Health = cardAsset.MaxHealth;
            Power = cardAsset.Power;
        }
        else if (CardScript is Skill)
        {
            Power = cardAsset.Power;
        }
        // -Alex C.
    }

    //Méthode à appeler si on instancie une carte sans lui donner une identité pré-définie. La carte est instanciée en tant que carte vierge,
    //puis son identité (la CarteRessource) est assignée, puis elle signale à ses composantes qu'elle a terminé son initialisation.
    public void InitializeFromCardAsset(CarteRessource asset)
    {
        cardAsset = asset;

        InitializeValues();

        if (Initialized != null)
        {
            Initialized();
        }
    }


    public void Discard()
    {
        GraveyardBehavior gy = Owner.GetComponentInChildren<GraveyardBehavior>();
        if (GetComponent<Draggable>() != null)
        {
            Draggable cardDraggable = GetComponent<Draggable>();
            cardDraggable.enabled = false;
            DraggingAction drga = GetComponent<DraggingAction>();
            cardDraggable.enabled = false;
            DragTarget drtrg = GetComponent<DragTarget>();
            drtrg.enabled = false;
            transform.SetParent(gy.transform);

        }
        
        
        gy.AddCardToGraveyard(this);
      

    }

    public void Play(CardManager target = null)
    {
        if (CardScript is IPlayable)
        {
            (CardScript as IPlayable).Play(target);
        }
    }
}


