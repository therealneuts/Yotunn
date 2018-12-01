using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImage : MonoBehaviour {

    CardManager cardManager;
    Image cardImage;

    // Use this for initialization
    void Start()
    {
        //Assignation des références.
        cardManager = GetComponentInParent<CardManager>();
        cardImage = GetComponent<Image>();

        cardImage.sprite = cardManager.CarteRessource.ImageRessource;
    }
}
