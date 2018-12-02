using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardName : MonoBehaviour {

    CardManager cardManager;
    TextMeshProUGUI nameText;

	// Use this for initialization
	void Start () {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        nameText = GetComponent<TextMeshProUGUI>();

        nameText.text = cardManager.CarteRessource.name;
	}
}
