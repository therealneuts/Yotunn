using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;


/// <summary>
/// Fait l'arrangement visuel de la main d'un joueur en arrangeant les cartes sur un cercle virtuel à distances égales.
/// </summary>
public class HandLayout : MonoBehaviour {

    //Les lots sont entrés manuellement dans l'éditeur Unity.
    public Transform[] slots;

    //Champs modifiables dans l'éditeur Unity qui affectent le positionnement des cartes.
    [SerializeField] float totalArc = Mathf.PI / 2;                         //Distance totale que les cartes peuvent couvrir, en radians.
    [SerializeField] float maxAngleBetweenCards = Mathf.PI / 16;            //Désigne une distance maximale entre chaque carte, en radians
    [SerializeField] float virtualCircleRadius = 5f;                        //Le rayon du cercle viruel autour duquel les cartes sont arrangées.
    [SerializeField] float zSpacing = 0.02f;                                //Superpose les cartes.

    Vector3 middleCardPosition;
    Vector3 virtualCenter;
    int numChildren;

    List<Transform> trimmedSlots = new List<Transform>();


    void Start ()
    {
        Player p = GetComponentInParent<Player>();

        p.Draw(5);
        ArrangeSlots();
    }

    /// <summary>
    /// Prend en charge le comportement de l'ajout d'une carte à la main d'un joueur.
    /// </summary>
    /// <param name="card">La carte à placer dans la main</param>
    public void AddCardToHand(CardManager card)
    {
        //Puisque la carte sera déplacée lorsqu'elle sera assignée à un lot parent et que celui-ci sera réordonné, on enregistre sa position initiale
        //afin de s'assurer que, visuellement, la carte sortira de la pile de carte.
        Vector3 init = card.transform.position;

        //Si la main est pleine, retourne.
        if (trimmedSlots.Count == slots.Length)
        {
            HandIsFull();
            return;
        }

        //Trouve un lot vide et assigne la carte à ce lot.
        foreach (Transform slot in slots)
        {
            if (slot.GetComponentInChildren<CardManager>() == null)
            {
                card.transform.SetParent(slot);
            }
        }

        //Ajoute et retire les composantes qui doivent ou ne doivent pas être actives quand une carte est dans la main du joueur.
        if (card.GetComponent<DragTarget>() != null)
        {
            card.GetComponent<DragTarget>().enabled = false;
        }
        if (card.GetComponent<Draggable>() == null)
        {
            card.gameObject.AddComponent<Draggable>();
        }
        else
        {
            card.GetComponent<Draggable>().enabled = true;
        }

        card.gameObject.SetActive(true);

        //Réordonne les lots. Puisque la carte est maintenant enfant d'un lot et suit ses mouvements et rotations, la carte sera déplacée.
        ArrangeSlots();

        //la carte est replacée sur la pile de cartes.
        card.transform.position = init;

        //Démarre l'effet visuel de la carte se déplaçant vers la main.
        StartCoroutine(MoveNewCard(card));
    }

    //Attend que tous les déplacements soient finis avant d'en ajouter un.
    private IEnumerator MoveNewCard(CardManager card)
    {
        List<Tween> activeTweens = DOTween.TweensByTarget(transform, false);
        while (activeTweens != null)
        {
            //Trouve le tween en train de manipuler l'objet qui a la plus longue durée, puis suspend jusqu'à ce que celui-ci soit fini.
            Tween longest = activeTweens.Where(t => t.Duration() == activeTweens.Max(tw => tw.Duration()))
                                        .First();
            yield return longest.WaitForCompletion();
            //Refait une recherche pour les objets qui sont en train de manipuler cet objet.
            activeTweens = DOTween.TweensByTarget(transform, false);
        }

        card.transform.DOLocalMove(Vector3.zero, GlobalSettings.instance.cardTransitionTime);
        card.transform.DOLocalRotate(Vector3.zero, GlobalSettings.instance.cardTransitionTime);
    }

    private void HandIsFull()
    {
        //TODO implement visual feedback
    }

    private void ArrangeSlots()
    {
        trimmedSlots.Clear();

        //Les lots vides de cartes ne sont pas considérés.
        foreach (Transform slot in slots)
        {
            if (slot.GetComponentInChildren<CardManager>() != null)
            {
                trimmedSlots.Add(slot);
            }
        }


        middleCardPosition = transform.position;
        numChildren = trimmedSlots.Count;

        //Création d'un cercle virtuel dont le centre est situé rayon unités en dessous ou au-dessus de la position initiale des lots.
        virtualCenter = middleCardPosition - new Vector3(0f, virtualCircleRadius, 0f);

        //Multiplié par le nombre de radians entre chaque carte, il donnera l'écart en radians entre le centre et la carte.
        float coefficient;
        //Angle en radians où la carte devra être placée.
        float angle;
        //Distance en radians entre chaque carte. Valeur minimale de 0, valeur maximale représentée par maxAngleBetweenCards.
        float radiansBetweenSegments = Mathf.Clamp((totalArc / numChildren), 0, maxAngleBetweenCards);

        for (int i = 0; i < numChildren; i++)
        {
            //Moyen primitif d'obtenir plus ou moins un série allant de -numChildren/2 à numChildren/2
            coefficient = i - ((numChildren - 1) / 2f);
            //Les cartes sont placées autour de l'angle PI/2 sur le cercle.
            angle = (Mathf.PI / 2) + coefficient * radiansBetweenSegments;

            //Distances en x et y du centre virtuel du cercle.
            float xOffset = Mathf.Cos(angle);
            float yOffset = Mathf.Sin(angle);

            //Assignation de la position et de la rotation de la carte sur le cercle virtuel. Le zSpacing est la position en profondeur de la carte.
            //Puisque les cartes sont superposées lorsqu'il y en a une grande quantité, on doit les séparer en pronfondeur.
            trimmedSlots[i].position = new Vector3(virtualCenter.x + virtualCircleRadius * xOffset, virtualCenter.y + virtualCircleRadius * yOffset, i * zSpacing);

            //Crée une rotation sur l'axe Z (profondeur) égale à l'angle de la carte sur le cercle. Cependant, puisqu'on veut que pi/2 (le haut du cercle) crée une rotation
            //de 0 (la carte est à la verticale et n'a pas besoin de tourner), on soustrait 90 degrées de l'angle sur le cercle.
            trimmedSlots[i].rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Rad2Deg * angle - 90f));
        }
    }

    //-Alex C.
}
