using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Détient toutes les références du textes et des Images sur la carte
// Elle informe au GameObject quelle information qu'elle doit prendre
public class CarteRessourceEnGameObject : MonoBehaviour {

    //Ici nous avons besoin d'une référence vers la CarteRessource du jeu pour contrôler les différentes propriétés
    //de l'objet dans le plan du jeu, soit le text les image, et les différentes propriétés.
    public CarteRessource CarteRessource;
    //Étant donné que'une carte transporte un preview, une copie conforme à la carte mais qui contient
    //seulement les propriétés graphiques de la carte donnée, le CarteRessourceEnGameObject doit pouvoir connaître celui
    //du preview, lequel transporte lui-même un CarteRessourceEnGameObject.
    public CarteRessourceEnGameObject CartePreview;
    [Header("Text Component References")]
    //Toutes les descriptions d'une carte
    public Text txtNom;
    public Text txtManaCout;
    public Text txtDescription;
    public Text txtVie;
    public Text txtAttaque;
    [Header("Image References")]
    //Tous les éléments graphiques d'une carte
    public Image ImageRubanHaut;
    public Image ImageRubanBas;
    public Image ImageCarteGraphique;
    public Image ImageCorps;
    public Image ImageDevantFrame;
    //Élément qui dit au joueur quelle type est la carte
    public Image ImageCarteType;
    //Éléments graphique qui sera vu lorsque nous activons, afin que nous puissons informer au joueur qu'il peut
    //faire une action avec celle-ci
    public SpriteRenderer ImageGlowDevant;
    public Image ImageGlowDerriere;
    [Header("Graphics that needs to be disabled")]
    //Les éléments qui devront être caché car ils ne seront pas utiles
    public GameObject GOvie;
    public GameObject GOattack;

    //La méthode Awake qui à été donné à travers l'héritage de la classe MonoBehavior qui se fait après l'instanciation
    //de tous les objets devant être instancié au début du jeu
    void Awake()
    {
        //Si une carte asset a été référenciée, et donc non null, l'instance de CarteRessourceEnGameObject sait
        //qu'il doit mettre toutes les informations dans le graphique de la carte sur le jeu
        if (CarteRessource != null)
            //La méthode qui insère toute l'information de la carte ressource dans la carte instancié du jeu
            CarteRessourceToGameGraphique();
    }

    /// <summary>
    /// Un propriété qui nous laissera jouer la carte et si elle peut être jouer une couleur de fond sera affiché pour
    /// que le joueur soit notifié
    /// </summary>
    private bool boPeutEtreJoué = false;
    public bool CanBePlayedNow
    {
        get
        {
            //si elle retourne vrai la carte pourra être jouer par le joueur
            return boPeutEtreJoué;
        }

        set
        {            
            boPeutEtreJoué = value;
            //La raison pour laquelle nous Hard Codons la propriété de CanBePlayedNow est parce qu'à chaque changement 
            //de celui-ci nous voulons copoier la valeur donnée à l'objet ImageGlowDevant, ce qui activera
            //notre belle image de fond pour notre carte
            ImageGlowDevant.enabled = value;
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
            ImageCarteType.sprite = CarteRessource.CarteType;
        }
        //Appliquer le nom de la carte selon la définition de la carte donnée
        txtNom.text = CarteRessource.name;
        //          le mana de la carte selon la définition de la carte donnée
        txtManaCout.text = CarteRessource.CoutMana.ToString();
        //          la description de la carte selon la définition de la carte donnée
        txtDescription.text = CarteRessource.Description;
        //          L'image de la carte selon la définirion de la carte donnée
        ImageCarteGraphique.sprite = CarteRessource.ImageRessource;

        //Si la carte a un maximum de vie de zéro nous savons que c'est un sortilège
        //celle-ci est charactèrisé par le fait qu'elle n'a pas de vie et 
        //qui ne reste pas sur le jeu apès son utilisation
        if (CarteRessource.MaxHealth != 0)
        {
            //Si elle est une créature le nombre d'attaque et de vie doit être affiché au joueur
            GOvie.active = true;
            GOattack.active = true;
            txtAttaque.text = CarteRessource.Attack.ToString();
            txtVie.text = CarteRessource.MaxHealth.ToString();
        }
        else
        {
            //Si elle n'est pas une créature ne pas afficher le graphique du health et de l'attaque
            GOvie.active = false;
            GOattack.active = false;
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
}


