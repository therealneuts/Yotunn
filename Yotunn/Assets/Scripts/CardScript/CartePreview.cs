﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

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
    Vector3 v3CarteScaleMult;
    //ici on applique le scale initiale dans l'éditeur
    Vector3 v3CarteInitialScale;

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

    [SerializeField] float yOffset = 2f;
    [SerializeField] float duration = 0.5f;

    Canvas[] cardCanvas;

    private void Start()
    {
        cardCanvas = GetComponentsInChildren<Canvas>();
        v3CarteInitialScale = CarteRectTransform.localScale;
        //Instancie un nouveau vecteur 3 selon les paramètres données
        //Celui-ci est donnée qu'une seul foix durant l'instance de cet objet
        v3CarteScaleMult = new Vector3(v3CarteInitialScale.x * flCarteScaleMult, v3CarteInitialScale.y * flCarteScaleMult, 1f);

        //si has Preview est vrai
        if (hasPreview)
        {
            //les instances de DelegateOnMouseOverAction vont pointer vers des
            //méthode anonyme
            //OnMouseOverAction += () => sequence.Append(CarteRectTransform.DOScale(v3CarteScaleMult, duration));
            //OnMouseOverAction += () => sequence.Append(CarteRectTransform.DOLocalMoveY(transform.localPosition.y + direction*yOffset, duration));
            //OnMouseLeaveACtion += () => sequence.Append(CarteRectTransform.DOScale(v3CarteInitialScale, duration));
            //OnMouseLeaveACtion += () => sequence.Append(CarteRectTransform.DOLocalMoveY(transform.localPosition.y, duration));
        }
    }

    //Cet événement est un événement qui à été passé du parent MonoBehaviour
    //Cette méthode est appelé à tout les fois que l'usagé place son curseur 
    //par dessus un objet appellé collider du GameObject de la carte
    private void OnMouseEnter()
    {
        //Donner un SortingOrder gigantesque à la carte assure qu'elle sera visible, même si d'autres objets sont plus près de la caméra.
        System.Array.ForEach(cardCanvas, c => c.sortingOrder = 255);
        StartCoroutine(BeginPreview());
    }

    //Lorsque le curseur sort du collider cette méthode est appelé
    //Il vient aussi de la classe de base
    private void OnMouseExit()
    {
        System.Array.ForEach(cardCanvas, c => c.sortingOrder = 0);

        StartCoroutine(EndPreview());
    }

    IEnumerator BeginPreview()
    {
        //y = 0 est la ligne du milieu du jeu. Si la carte est dans la partie supérieure du jeu (joueur 2), elle grossit vers le bas.
        //Si elle est dans la partie inférieure du jeu, elle grossit vers le haut.
        int direction = transform.position.y > 0 ? -1 : 1;

        //Crée une liste des objets Tween qui sont présentement en train de manipuler cet objet. Tant que l'objet est en train d'être manipulé, la coroutine se suspend.
        List<Tween> activeTweens = DOTween.TweensByTarget(transform, false);
        while (activeTweens != null)
        {
            //Trouve le tween en train de manipuler l'objet qui a la plus longue durée, puis suspend jusqu'à ce que celui-ci soit fini.
            Tween longest = activeTweens.Where(t => t.Duration() == activeTweens.Max(tw => tw.Duration()))
                                        .First();
            yield return longest.WaitForCompletion();
            activeTweens = DOTween.TweensByTarget(transform, false);
        }

        //Applique les manipulations à la carte.
        CarteRectTransform.DOScale(v3CarteScaleMult, duration);
        CarteRectTransform.DOLocalMoveY(transform.localPosition.y + direction * yOffset, duration);
    }

    IEnumerator EndPreview()
    {
        CarteRectTransform.DOScale(v3CarteInitialScale, duration);
        List<Tween> activeTweens = DOTween.TweensByTarget(transform, false);
        while (activeTweens != null)
        {
            Tween longest = activeTweens.Where(t => t.Duration() == activeTweens.Max(tw => tw.Duration()))
                                        .First();
            yield return longest.WaitForCompletion();
            activeTweens = DOTween.TweensByTarget(transform, false);
        }
        CarteRectTransform.DOLocalMoveY(transform.localPosition.y, duration);
    }
}
//Yan, Alex C