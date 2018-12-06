using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using DG.Tweening;

public class ButtonHoverAction : MonoBehaviour {

    [Header("L'image pour le hallow")]
    //Référence vers le gameObject du jeu
    public GameObject Hallow;

	// Use this for initialization
	void Start () {        
        
	}

    private void OnMouseOver()
    {
        //Active le gameObject afin que nous puissons voir le glow du button
        Hallow.SetActive(true);
    }
    private void OnMouseExit()
    {
        //Désactive lorsque l'usager leave la case du collider afin d'avoir un bon user interaction
        Hallow.SetActive(false);
    }

}
