using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardType : MonoBehaviour {

    //La liste des images. La bonne image sera choisie parmi celles-ci et affiché par ce script.
    [SerializeField] Sprite avatarImg;
    [SerializeField] Sprite creatureImg;
    [SerializeField] Sprite skillImg;
    [SerializeField] Sprite boostImg;
    [SerializeField] Sprite statusImg;

    /*
        Référence au CardManager qui se trouve au sommet d'une carte et qui sert d'intermédiaire entre tous les éléments de celle-ci.
         */
    CardManager cardManager = null;

    //Référence à l'image qui se trouve sur l'objet.
    Image imageComponent = null;

    // Use this for initialization
    private void Awake()
    {
        /*
            Grosso modo, le script demande au parent: "quel type de carte suis-je?" Ensuite, il affiche la bonne image pour ce type.
         */

        //Fonction de unity qui remonte la hiérarchie d'un objet pour trouver un script. Les méthodes GetComponent sont les principales
        //Méthodes d'assignation de références à des scripts se trouvant sur un objet.
        cardManager = GetComponentInParent<CardManager>();

        //GetComponent, elle, cherche un script --sur le même objet--
        imageComponent = GetComponent<Image>();

        //Prépare son initialisation en plaçant sa méthode OnCardInitialized dans l'événement Initialized du parent de la carte.
        //Puisque Initialized n'est jamais lancé avant que le parent ait découvert son identité, ce système évite tout problème
        //Pouvant survenir de l'ordre d'exécution des choses.
        cardManager.Initialized += OnCardInitialized;
    }

    private void OnCardInitialized()
    {
        //Interroge le CardAsset du cardManager pour connaître le type.
        CardTypes type = cardManager.cardAsset.type;

        //Assigne la bonne image à sa composante qui affiche une image.
        switch (type)
        {
            case CardTypes.Avatar:
                imageComponent.sprite = avatarImg;
                break;
            case CardTypes.Creature:
                imageComponent.sprite = creatureImg;
                break;
            case CardTypes.Skill:
                imageComponent.sprite = skillImg;
                break;
            case CardTypes.Boost:
                imageComponent.sprite = boostImg;
                break;
            case CardTypes.Status:
                imageComponent.sprite = statusImg;
                break;
            default:
                throw new System.Exception("Unexpected card type sent to CardType object");
        }
    }

    /*
        -Alex C
     */
}
