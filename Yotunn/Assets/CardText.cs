using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardText : MonoBehaviour {

    CardManager cardManager;
    Text rulesText;

    // Use this for initialization
    void Start()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        rulesText = GetComponent<Text>();

        rulesText.text = cardManager.CarteRessource.Description;
    }
}
