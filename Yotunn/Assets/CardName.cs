using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardName : MonoBehaviour {

    CardManager cardManager;
    Text nameText;

	// Use this for initialization
	void Start () {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        nameText = GetComponent<Text>();

        nameText.text = cardManager.CarteRessource.name;
	}
}
