using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardText : MonoBehaviour {

    CardManager cardManager;
    TextMeshProUGUI rulesText;

    // Use this for initialization
    void Start()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        rulesText = GetComponent<TextMeshProUGUI>();

        rulesText.text = cardManager.CarteRessource.Description;
    }
}
