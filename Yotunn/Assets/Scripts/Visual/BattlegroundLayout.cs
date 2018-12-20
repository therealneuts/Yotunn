using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

/// <summary>
/// Fait l'arragement visuel des cartes sur la table de jeu. Contrairement à l'arrangement de la main, on leur donne ici un arragement horizontal.
/// </summary>
public class BattlegroundLayout : MonoBehaviour {

    //Les lots sont entrés manuellement dans l'éditeur Unity.
    public Transform[] slots;

    [SerializeField] float totalDistance = 18f;
    [SerializeField] float maxDistanceBetweenCards = 3f;

    float minDistanceBetweenCards;
    Vector3 center;
    List<Transform> trimmedSlots = new List<Transform>();

    // Use this for initialization
    void Start ()
    {
        //Transform.position est la position de l'objet parent des lots.
        center = transform.position;
        ArrangeCards();
    }

    private void ArrangeCards()
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

        //S'il n'y a aucune carte sur le terrain, la composante n'a plus de travail.
        if (trimmedSlots.Count == 0) { return; }

        //Ce petit segment de code nous permet de trouver dynamiquement la largeur des cartes. Pour améliorer la qualité des contrôles, on ne veut pas que les cartes
        //soient superposées, alors nous trouvons une distance minimale entre les cartes à partir de leur largeur.
        BoxCollider cardCollider = trimmedSlots[0].GetComponentInChildren<BoxCollider>();
        minDistanceBetweenCards = cardCollider.size.x;

        //coefficient multipliera distanceBetweenSegments pour obtenir la distance de la carte par rapport au centre.
        float coefficient;
        float distanceFromCenter;

        //Mathf.Clamp limite la valeur d'un nombre entre deux bornes. Ici, on donne un limite à la distance maximale entre deux cartes pour éviter
        //qu'elles soient trop écartées lorsqu'il y en a peu, et une limite à la distance minimale pour perfectionner les contrôles.
        int numCards = trimmedSlots.Count;
        float distanceBetweenSegments = Mathf.Clamp((totalDistance / numCards), minDistanceBetweenCards, maxDistanceBetweenCards);

        //Positionne les cartes sur la table en calculant leur distance du centre.
        for (int i = 0; i < numCards; i++)
        {
            coefficient = i - ((numCards - 1f) / 2f);
            distanceFromCenter = coefficient * distanceBetweenSegments;
            trimmedSlots[i].position = new Vector3(center.x + distanceFromCenter, center.y, center.z);
        }
    }

    public void PlaceCardOnBattleground(CardManager card)
    {
        Transform targetSlot = null;
        foreach (Transform slot in slots)
        {
            if (slot.GetComponentInChildren<CardManager>() == null)
            {
                targetSlot = slot;
                break;
            }
        }

        if (targetSlot == null)
        {
            throw new System.Exception("Battleground is full!");
        }

        card.transform.SetParent(targetSlot);

        if (card.GetComponent<DragTarget>() == null)
        {
            card.gameObject.AddComponent<DragTarget>();
        }
        else
        {
            card.GetComponent<DragTarget>().enabled = true;
        }

        ArrangeCards();

        StartCoroutine(MoveToBattleground(card)); //exécute la coroutine MovetoBattleground avec la carte joué en paramètre, pour placer la carte joué dans la slot demandé
    }

    private IEnumerator MoveToBattleground(CardManager card)
    {
        List<Tween> activeTweens = DOTween.TweensByTarget(card.transform, false);
        while (activeTweens != null)
        {
            Tween longest = activeTweens.Where(t => t.Duration() == activeTweens.Max(tw => tw.Duration()))
                                        .First();
            yield return longest.WaitForCompletion();
            activeTweens = DOTween.TweensByTarget(transform, false);
        }

        card.transform.DOMove(card.transform.parent.position, GlobalSettings.instance.cardTransitionTime);
        card.transform.DORotate(Vector3.zero, GlobalSettings.instance.cardTransitionTime);
    }
}
//-Alex C
