using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImage : MonoBehaviour {

    CardManager cardManager;
    Image cardImage;


    void Awake()
    {
        //Assigne les références à son parent et à sa composante Image.
        cardManager = GetComponentInParent<CardManager>();
        cardImage = GetComponent<Image>();

        //Prépare son initialisation en plaçant sa méthode OnCardInitialized dans l'événement Initialized du parent de la carte.
        //Puisque Initialized n'est jamais lancé avant que le parent ait découvert son identité, ce système évite tout problème
        //Pouvant survenir de l'ordre d'exécution des choses.
        cardManager.Initialized += OnCardInitialized;
    }

    void OnCardInitialized()
    {
        //Va chercher l'image que cet objet devrait afficher et l'assigne à sa composante Image pour l'Afficher.
        cardImage.sprite = cardManager.cardAsset.ImageRessource;
    }
    //Alex C
}
