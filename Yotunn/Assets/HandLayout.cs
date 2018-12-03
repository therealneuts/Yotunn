using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Fait l'arrangement visuel de la main d'un joueur en arrangeant les cartes sur un cercle virtuel à distances égales.
/// </summary>
public class HandLayout : MonoBehaviour {

    //Les lots sont entrés manuellement dans l'éditeur Unity.
    public Transform[] slots;

    //Champs modifiables dans l'éditeur Unity qui affectent le positionnement des cartes.
    [SerializeField] float totalArc = Mathf.PI / 2;
    [SerializeField] float maxAngleBetweenCards = Mathf.PI / 16;
    [SerializeField] float virtualCircleRadius = 5f;
    [SerializeField] float zSpacing = 0.02f;

    Vector3 middleCardPosition;
    Vector3 virtualCenter;
    int numChildren;

    List<Transform> trimmedSlots = new List<Transform>();


    void Start () {
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
            coefficient = i - ((numChildren -1) / 2f);
            //Les cartes sont placées autour de l'angle PI/2 sur le cercle.
            angle = (Mathf.PI / 2) + coefficient * radiansBetweenSegments;

            //Distances en x et y du centre virtuel du cercle.
            float xOffset = Mathf.Cos(angle);
            float yOffset = Mathf.Sin(angle);

            //Assignation de la position et de la rotation de la carte sur le cercle virtuel. Le zSpacing est la position en profondeur de la carte.
            //Puisque les cartes sont interposées lorsqu'il y en a une grande quantité, on doit les séparer en pronfondeur.
            trimmedSlots[i].position = new Vector3(virtualCenter.x + virtualCircleRadius * xOffset, virtualCenter.y + virtualCircleRadius * yOffset, i * zSpacing);

            //Crée une rotation sur l'axe Z (profondeur) égale à l'angle de la carte sur le cercle. Cependant, puisqu'on veut que pi/2 (le haut du cercle) crée une rotation
            //de 0 (la carte est à la verticale et n'a pas besoin de tourner), on soustrait 90 degrées de l'angle sur le cercle.
            trimmedSlots[i].rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Rad2Deg * angle - 90f));
        }
    }
	
	//-Alex C.
}
