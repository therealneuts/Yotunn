using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;
using TMPro;

public class CardCost : MonoBehaviour {

    CardManager cardManager = null;
    int cost, initialCost;
    TextMeshProUGUI costText = null;
    Cards.Carte type;

    // Use this for initialization
    void Start()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        costText = GetComponent<TextMeshProUGUI>();
        type = cardManager.CardScript;

        //Les cartes qui ne sont pas jouables n'ont pas besoin de cette composante.
        if (!(type is IPlayable))
        {
            gameObject.SetActive(false);
            return;
        }

        //Initialisation
        cost = cardManager.ManaCost;
        initialCost = cost;
        costText.text = cost.ToString();

        //Assignation de la méthode OnCostChanged à l'événement ManaCostChanged du cardManager.
        cardManager.ManaCostChanged += OnCostChanged;

    }

    //Fonction qui prendra en charge l'événement ManaCostChanged de la carte en affichant la nouvelle valeur.
    public void OnCostChanged(CardManager card, int difference)
    {
        cost += difference;
        if (cost < 0) { cost = 0; }

        costText.text = cost.ToString();

        if (cost < initialCost) { costText.color = Color.green; }
        else if (cost > initialCost) { costText.color = Color.red; }
        else { costText.color = Color.white; }
    }

    /*
        -Alex C.
     */
}
