using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CartePreview : MonoBehaviour {
    //Propriété statique afin de créer un singleton, lequel fera certain qu'un
    //joueur ne puisse activer cet événement
    private static GameObject CarteEnPreview = null;

    //le float qui sera appliqué donnera le scale que doit prendre la partie
    //graphique de la carte en l'enrigistrent en float on peut le transferer
    //a un vecteur3 plus facilement
    [Header("Appliquer un scale")]
    public float flCarteScaleMult;
    //le vecteur3 sera configuré par flCarteScaleMult
    private Vector3 v3CarteScaleMult;
    //ici on applique le scale initiale dans l'éditeur
    public Vector3 v3CarteInitialScale;

    [Header("Besoin d'une carte ici")]
    public GameObject Carte; //Servira pour le singleton
    [Header("Besoin d'une rectTransform ici")]
    //Le rect transforme est le parent de tout les composants graphiques de la carte
    //Alors si je scale celui-ci je mannipule toute les composants graphiques aussi
    public RectTransform CarteRectTransform;

    private void Start()
    {
        //Debug.Log(CarteRectTransform.localScale); 
        //Instancie un nouveau vecteur 3 selon les paramètres données
        //Celui-ci est donnée qu'une seul foix durant l'instance de cet objet
        v3CarteScaleMult = new Vector3(flCarteScaleMult, flCarteScaleMult);
    }

    //Cet événement est un événement qui à été passé du parent MonoBehaviour
    //Cette méthode est appelé à tout les fois que l'usagé place son curseur 
    //par dessus un objet appellé collider du GameObject de la carte
    private void OnMouseOver()
    {
        //Applique le nouveau scale dans le CarteRectTransform
        //Selon la valeur du V3 et selon une vitesse de 1f, lequel
        //est permis par using DG.Tweening
        CarteRectTransform.DOScale(v3CarteScaleMult, 1f);

        //if (CarteEnPreview == null)
        //{
        //    CarteEnPreview = Carte;
        //    CarteRectTransform.transform.localScale = new Vector3(1, 1, 1);
        //    Debug.Log("v3Initiale:" + CarteRectTransform.transform.localScale);
        //    CarteRectTransform.transform.DOScale(v3CarteScaleMult, 1f);
        //}
        //else
        //{
        //    CarteEnPreview.transform.localScale = new Vector3(1, 1, 1);
        //    CarteEnPreview = null;


        //}
    }

    //Lorsque le curseur sort du collider cette méthode est appelé
    //Il vient aussi de la classe de base
    private void OnMouseExit()
    {
        //CarteEnPreview = null;
        //CarteRectTransform.transform.DOScale(new Vector3(1,1,1), 1f);

        //rend la carte sous son scale initiale
        CarteRectTransform.DOScale(v3CarteInitialScale, 1f);
    }
}
