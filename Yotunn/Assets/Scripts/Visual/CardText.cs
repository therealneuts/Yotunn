using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardText : MonoBehaviour {

    CardManager cardManager;
    TextMeshProUGUI rulesText;

    // Use this for initialization
    void Awake()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        rulesText = GetComponent<TextMeshProUGUI>();

        //Prépare son initialisation en plaçant sa méthode OnCardInitialized dans l'événement Initialized du parent de la carte.
        //Puisque Initialized n'est jamais lancé avant que le parent ait découvert son identité, ce système évite tout problème
        //Pouvant survenir de l'ordre d'exécution des choses.
        cardManager.Initialized += OnCardInitialized;
    }

    private void OnCardInitialized()
    {
        rulesText.text = cardManager.cardAsset.Description; //Assigne à la carte le texte emmagasiné dans le cardAsset à la description
    }
}
