using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;
using TMPro;

public class CardHealth : MonoBehaviour {

    CardManager cardManager = null;
    int health;
    TextMeshProUGUI healthText = null;

	// Use this for initialization
	void Start () {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();

        Carte type = cardManager.CardScript;
        if (type is Cards.Entity)
        {

            //Initialisation de la vie.
            health = cardManager.Health;
            healthText.text = health.ToString();

            //Assignation de la méthode OnHealthChanged à l'événement HealthChanged du cardManager.
            cardManager.HealthChanged += OnHealthChanged;
        }
        else
        {
            gameObject.SetActive(false);
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
