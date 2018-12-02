using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHorizontalLayout : MonoBehaviour {

    //Array des slots pour la disposition des cartes dans la main
    public Transform[] CardSlots;

    private void Start()
    {
        //Le GetComponentsInChildren retoourne tout les composants
        //du type dans le GameObject ou dans ses enfants
        CardSlots = this.GetComponentsInChildren<Transform>();

        //S'il y a moins que 1 élément dans le tableau
        //Cela veut dire qu'il est pas nécessaire de faire le calcule de distance
        if (CardSlots.Length > 1)
        {
            //Prend la position du deuxième, le premier élément est l'emplcement de CardHorizontalLayout, donc l'ignorer et dernier élément de la liste le sauvegarde en Vecteur 3 
            Vector3 firstSlot = CardSlots[1].position;
            Vector3 lastSlot = CardSlots[CardSlots.Length -1].position;

            //Calcule du nombre de segment qu'aura n'autre main     
            float NumberOfSegment = (float)CardSlots.Length - 2f;

            //Selon la distance du premier et du dernier élément nous changerons sa position
            //Selon le nombre de segment que n'autre main comportera
            //i.e: DistanceX/NumberOfSegment => 6/8 divisera la place pour le nombre d'élément que nous avons
            float XDist = (lastSlot.x - firstSlot.x) / NumberOfSegment;
            float ZDist = (lastSlot.z - firstSlot.z) / NumberOfSegment;

            //Range les deux variables calculées dans une instance Vecteur 3
            Vector3 Dist = new Vector3(XDist, 0f, ZDist);

            //Nous nous référons au deuxième objet, alors nous mettons le deuxième élément de côté
            for (int i = 2; i < CardSlots.Length; i++)
            {
                //Pour tous éléments de la liste changer sa dispositon
                //en le translatant par rapport à son dernier en ajoutant la ditance calculé
                //plus haut
                CardSlots[i].position = CardSlots[i - 1].position + Dist;
            }
        }

    }

    private void Awake()
    {
    }
}
