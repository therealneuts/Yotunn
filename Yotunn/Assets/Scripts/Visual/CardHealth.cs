using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;
using TMPro;

public class CardHealth : MonoBehaviour {

    CardManager cardManager = null; //Crée une variable cardmanager null qui sera défini plus dans la fonction Awake
    int health;
    TextMeshProUGUI healthText = null; //Crée une variable Text null qui sera défini plus dans la fonction Awake

    // Use this for initialization
    void Awake ()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();


        cardManager.Initialized += OnCardInitialized; //Appel le délégué Initialized.
    }

    private void OnCardInitialized() 
    {
        Carte type = cardManager.CardScript; //Trouve le type de la carte à partir du cardscript du Manager
        if (type is Cards.Entity) //Si la carte est une entity
        {

            //Initialisation de la vie.
            health = cardManager.Health; 
            healthText.text = health.ToString();

            //Assignation de la méthode OnHealthChanged à l'événement HealthChanged du cardManager.
            cardManager.HealthChanged += OnHealthChanged;
        }
        else
        {
            gameObject.SetActive(false); //Rend l'objet inactif
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    //Fonction qui prendra en charge l'événement HealthChanged de la carte en affichant la nouvelle valeur.
    public void OnHealthChanged(CardManager card, int difference)
    {
        health += difference;
        if (health < 0) { health = 0; }

        healthText.text = health.ToString();
    }

    /*
        -Alex C.
     */
}
