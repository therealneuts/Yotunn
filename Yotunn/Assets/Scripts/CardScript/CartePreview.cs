using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CartePreview : MonoBehaviour {
    //Référence statique lequel fera certain qu'un
    //joueur ne puisse activer cet événement plsu d'une fois lorsque c'est activé
    private static bool CarteEnPreview = false;
    
    [Header("Indique si la carte à un préview")]
    public bool hasPreview; //Si vrai la carte pourra donner un preview

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
    public GameObject Carte; //Servira comme référence pour savoir si une est activé
    [Header("Besoin d'une rectTransform ici")]
    //Le rect transforme est le parent de tout les composants graphiques de la carte
    //Alors si je scale celui-ci je mannipule toute les composants graphiques aussi
    public RectTransform CarteRectTransform;

    //Délegué d'évenement qui est appelé lorsque OnMouse est appelé
    public delegate void DelegateOnMouseOverAction();
    //Les différents événements, que si la carte a un hasPreview il son appelé 
    public event DelegateOnMouseOverAction OnMouseOverAction;
    public event DelegateOnMouseOverAction OnMouseLeaveACtion;

    private void Start()
    {
        //Debug.Log(CarteRectTransform.localScale); 
        //Instancie un nouveau vecteur 3 selon les paramètres données
        //Celui-ci est donnée qu'une seul foix durant l'instance de cet objet
        v3CarteScaleMult = new Vector3(flCarteScaleMult, flCarteScaleMult);

        //si has Preview est vrai
        if (hasPreview)
        {
            //les instances de DelegateOnMouseOverAction vont pointer vers des
            //méthode anonyme
            OnMouseOverAction += () => CarteRectTransform.DOScale(v3CarteScaleMult, 1f);
            OnMouseLeaveACtion += () => CarteRectTransform.DOScale(v3CarteInitialScale, 1f);
        }
    }

    //Cet événement est un événement qui à été passé du parent MonoBehaviour
    //Cette méthode est appelé à tout les fois que l'usagé place son curseur 
    //par dessus un objet appellé collider du GameObject de la carte
    private void OnMouseOver()
    {
        //Applique le nouveau scale dans le CarteRectTransform
        //Selon la valeur du V3 et selon une vitesse de 1f, lequel
        //est permis par using DG.Tweening
        if(!CarteEnPreview)
        {
            CarteEnPreview = true;
            //Si l'action over n'est pas null elle est appelé
            if (OnMouseOverAction != null)
                OnMouseOverAction();
        }
    }

    //Lorsque le curseur sort du collider cette méthode est appelé
    //Il vient aussi de la classe de base
    private void OnMouseExit()
    {
        CarteEnPreview = false;

        //rend la carte sous son scale initiale
        if (OnMouseLeaveACtion != null)
            //Si l'action leave n'est pas null elle est appelé
            OnMouseLeaveACtion();
    }
}
//Yan