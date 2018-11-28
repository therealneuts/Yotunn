using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Détient toutes les références du textes et des Images sur la carte
public class OneCardManager : MonoBehaviour {

    //Ici nous avons besoin d'une référence vers la CarteRessource du jeu pour contrôler les différentes propriétés
    //de l'objet dans le plan du jeu, soit le text les image, et les différentes propriéyés.
    public CardAsset cardAsset;
    //Étant donné que'une carte transporte un preview, une copie conforme à la carte mais qui contient
    //seulement les propriétés graphiques de la carte donnée, le OneCardManager doit pouvoir connaître celui
    //du preview, lequel transporte lui-même un OneCardManager.
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    //Toutes les descriptions d'une carte
    public Text NameText;
    public Text ManaCostText;
    public Text DescriptionText;
    public Text HealthText;
    public Text AttackText;
    [Header("Image References")]
    //Tous les éléments graphiques d'une carte
    public Image CardTopRibbonImage;
    public Image CardLowRibbonImage;
    public Image CardGraphicImage;
    public Image CardBodyImage;
    public Image CardFaceFrameImage;
    //Éléments graphique qui sera vu lorsque nous activons, afin que nous puissons informer au joueur qu'il peut
    //faire une action avec celle-ci
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;

    //La méthode Awake qui à été donné à travers l'héritage de la classe MonoBehavior qui se fait après l'instanciation
    //de tous les objets devant être instancié au début du jeu
    void Awake()
    {
        //Si une carte asset a été référenciée, et donc non null, l'instance de OneCardManager sait
        //qu'il doit mettre toutes les informations dans le graphique de la carte sur le jeu
        if (cardAsset != null)
            //La méthode qui insère toute l'information de la carte ressource dans la carte instancié du jeu
            ReadCardFromAsset();
    }

    /// <summary>
    /// Un propriété qui nous laissera jouer la carte et si elle peut être jouer une couleur de fond sera affiché pour
    /// que le joueur soit notifié
    /// </summary>
    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            //si elle retourne vrai la carte pourra être jouer par le joueur
            return canBePlayedNow;
        }

        set
        {            
            canBePlayedNow = value;
            //La raison pour laquelle nous Hard Codons la propriété de CanBePlayedNow est parce qu'à chaque changement 
            //de celui-ci nous voulons copoier la valeur donnée à l'objet CardFaceGlowImage, ce qui activera
            //notre belle image de fond pour notre carte
            CardFaceGlowImage.enabled = value;
        }
    }

    public void ReadCardFromAsset()
    {
        //Sera fait pour toutes les cartes
        
        if (cardAsset.characterAsset != null)
        {
            //Si CharacterAsset n'est pas null cela veut dire que c'est un character, alors nous mettons de la couleur
            //lesquelles sont prédéfini selon une classe Color32 une classe d'Unity qui représente des couleurs en format 32 bit
            //et laquelles est défini par la classe de character
            //Cela change tout le look et le feel de la classe
            CardBodyImage.color = cardAsset.characterAsset.ClassCardTint;
            CardFaceFrameImage.color = cardAsset.characterAsset.ClassCardTint;
            CardTopRibbonImage.color = cardAsset.characterAsset.ClassRibbonsTint;
            CardLowRibbonImage.color = cardAsset.characterAsset.ClassRibbonsTint;
        }
        else
        {
            //CardBodyImage.color = GlobalSettings.Instance.CardBodyStandardColor;
            CardFaceFrameImage.color = Color.white;
            //CardTopRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
        }
        //Appliquer le nom de la carte selon la définition de la carte donnée
        NameText.text = cardAsset.name;
        //          le mana de la carte selon la définition de la carte donnée
        ManaCostText.text = cardAsset.ManaCost.ToString();
        //          la description de la carte selon la définition de la carte donnée
        DescriptionText.text = cardAsset.Description;
        //          L'image de la carte selon la définirion de la carte donnée
        CardGraphicImage.sprite = cardAsset.CardImage;

        //Si la carte a un maximum de vie de zéro nous savons que c'est un sortilège
        //celle-ci est charactèrisé par le fait qu'elle n'a pas de vie et 
        //qui ne reste pas sur le jeu apès son utilisation
        if (cardAsset.MaxHealth != 0)
        {
            //Si elle est une créature le nombre d'attaque et de vie doit être affiché au joueur
            AttackText.text = cardAsset.Attack.ToString();
            HealthText.text = cardAsset.MaxHealth.ToString();
        }


        //si le PreviewManager est null donc nous savons que celui-ci est un l'objet
        //de la carte et non le deuxième instancié
        if (PreviewManager != null)
        {
            //Afin que nous n'ayons pas une boucle qui ne finirait jamais 
            //le preview devrait avoir un OneCardManager aussi, mais celui-ci devrait être null
            PreviewManager.cardAsset = cardAsset;
            //Appeler la  méthode readCardFromAsset() du preview
            PreviewManager.ReadCardFromAsset();
        }
    }
}


