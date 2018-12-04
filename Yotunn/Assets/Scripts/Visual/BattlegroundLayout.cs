using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start () {
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

        //Transform.position est la position de l'objet parent des lots.
        center = transform.position;

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
            print(distanceFromCenter + " " + distanceBetweenSegments);
            trimmedSlots[i].position = new Vector3(center.x + distanceFromCenter, center.y, center.z);
        }

        //-Alex C
    }
}
