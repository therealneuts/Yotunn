using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//Class hérité de la classe abstraite DraggingAction
public class DraggingHerited : DraggingAction {

    //Vector3 qui sauvegardera la position initiale du début du dragging
    private Vector3 v3PositionInitiale;
    
    //Override qui sauvegarde la position de départ avant le début du dragging afin de le remettre à ça position initiale
    //Soit dans la main du joueur ou à l'endroit ou la carte était dans le jeu
    public override void OnStartDrag()
    {
        //transform qui nous indique la position de l'objet dans le plan du jeu
        v3PositionInitiale = this.transform.position;
    }
    //Nous retournons la carte à ça position du départ selon une méthode d'une librairie utilisé afin de crée de beau mouvement
    public override void OnEndDrag()
    {
        //DOMove change la position en fesant une transition à l'objet dans le jeu vers la position donnée au premier paramètre
        //à une vitesse donnée comme deuxième paramètre
        //.SetEase est une méthode qui est appelé pour dire comment la transition se fera
        transform.DOMove(v3PositionInitiale, 1f);/*.SetEase(Ease.OutBounce, .5f, .1f); // <-- http://www.easings.net*/
    }

    public override void OnDraggingInUpdate()
    {
    }


    protected override bool DragSuccessful()
    {
        //retourne vrai pour dire que la carte à bien été bougé
        return true;
    }
}
//Yan