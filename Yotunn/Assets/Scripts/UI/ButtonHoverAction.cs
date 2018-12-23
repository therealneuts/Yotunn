using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using DG.Tweening;

public class ButtonHoverAction : MonoBehaviour {

    [Header("L'image pour le hallow")]
    //Référence vers le gameObject du jeu
    public GameObject Hallow;

    private void OnMouseOver()
    {
        //Active le gameObject afin que nous puissions voir le glow du button
        Hallow.SetActive(true);
    }
    private void OnMouseExit()
    {
        //Désactive lorsque l'usager quitte la case du collider afin d'avoir une bonne interaction avec l'usager
        Hallow.SetActive(false);
    }

}
