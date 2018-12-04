using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;
using TMPro;

public class CardPower : MonoBehaviour {

    CardManager cardManager = null;
    int initialPower, power;
    TextMeshProUGUI powerText = null;
    CardManager avatar;
    Carte type;

    // Use this for initialization
    void Start()
    {

        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        powerText = GetComponent<TextMeshProUGUI>();
        //avatar = cardManager.Avatar;
        type = cardManager.CardScript;

        //Seulement les habiletés qui font des dégats ont besoin d'utiliser cet objet.
        if (!(type is IDamaging))
        {
            gameObject.SetActive(false);
            return;
        }

        //La puissance des habiletés dépend de celle de l'avatar, alors il faut discriminer.
        if (type is Skill)
        {
            initialPower = cardManager.Power;
            power = initialPower;
            powerText.text = power.ToString();
        }
        else if (type is Entity)
        {
            initialPower = cardManager.Power;
            power = initialPower;
            powerText.text = power.ToString();
        }

        //Assignation de la méthode OnPowerChanged à l'événement PowerChanged du cardManager.
        cardManager.PowerChanged += OnPowerChanged;

        //Puisqu'une abilité dépend de la force de son avatar, OnPowerChanged est aussi ajoutée à Avatar
        if (type is Skill)
        {
            //avatar.PowerChanged += OnPowerChanged;
        }
    }

    //Fonction qui prendra en charge l'événement PowerChanged de la carte en affichant la nouvelle valeur.
    public void OnPowerChanged(CardManager card, int difference)
    {
        power += difference;

        powerText.text = power.ToString();
        if (power < initialPower) { powerText.color = Color.red; }
        else if (power > initialPower) { powerText.color = Color.green; }
        else { powerText.color = Color.white; }
    }

    /*
        -Alex C.
     */
}
