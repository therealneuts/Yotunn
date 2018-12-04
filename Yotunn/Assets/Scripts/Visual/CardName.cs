using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardName : MonoBehaviour {

    CardManager cardManager;
    TextMeshProUGUI nameText;

    // Use this for initialization
    void Awake()
    {
        //Assigne les références à son parent et à sa composante Image.
        cardManager = GetComponentInParent<CardManager>();
        nameText = GetComponent<TextMeshProUGUI>();

        //Prépare son initialisation en plaçant sa méthode OnCardInitialized dans l'événement Initialized du parent de la carte.
        //Puisque Initialized n'est jamais lancé avant que le parent ait découvert son identité, ce système évite tout problème
        //Pouvant survenir de l'ordre d'exécution des choses.
        cardManager.Initialized += OnCardInitialized;
    }

    void OnCardInitialized()
    {
        //Trouve et assigne le nom.
        nameText.text = cardManager.cardAsset.name;
    }
    //Alex C.
}
