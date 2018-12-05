using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//Class hérité de la classe abstraite DraggingAction
public class SkillDraggingBehavior : DraggingAction {

    [SerializeField] float duration = 0.2f;
    //Vector3 qui sauvegardera la position initiale du début du dragging
    private Vector3 v3PositionInitiale;
    TargetingOptions TargetingOptions;
    DragTarget target;
    Player targetablePlayer;
    RaycastHit[] hits;



    //Override qui sauvegarde la position de départ avant le début du dragging afin de le remettre à ça position initiale
    //Soit dans la main du joueur ou à l'endroit ou la carte était dans le jeu
    public override void OnStartDrag()
    {
        TargetingOptions = cardBeingDragged.cardAsset.Targets;

        switch (TargetingOptions)
        {
            case TargetingOptions.AllCreatures:
                targetablePlayer = null;
                break;
            case TargetingOptions.EnemyCreatures:
                targetablePlayer = cardBeingDragged.Owner.Enemy;
                break;
            case TargetingOptions.YourCreatures:
                targetablePlayer = cardBeingDragged.Owner;
                break;
            default:
                break;

        }
        //transform qui nous indique la position de l'objet dans le plan du jeu
        v3PositionInitiale = this.transform.position;
    }
    //Nous retournons la carte à ça position du départ selon une méthode d'une librairie utilisé afin de crée de beau mouvement
    public override void OnEndDrag()
    {
        bool targetIsLegal = DragSuccessful();
        if (targetIsLegal) { print("Dragged over a legal target!"); }
        else { print("Dragged to an illegal target!"); }
        //DOMove change la position en fesant une transition à l'objet dans le jeu vers la position donnée au premier paramètre
        //à une vitesse donnée comme deuxième paramètre
        //.SetEase est une méthode qui est appelé pour dire comment la transition se fera
        transform.DOMove(v3PositionInitiale, duration);/*.SetEase(Ease.OutBounce, .5f, .1f); // <-- http://www.easings.net*/
    }

    public override void OnDraggingInUpdate()
    {
        DrawLine();
        hits = Physics.RaycastAll(  ray: Camera.main.ScreenPointToRay(Input.mousePosition),
                                    maxDistance: 30f    );
    }


    protected override bool DragSuccessful()
    {
        if (TargetingOptions == TargetingOptions.NoTarget)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.GetComponent<DragTarget>() != null) { return true; }
            }
        }
        else
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.GetComponent<DragTarget>() != null)
                {
                    target = hit.collider.GetComponent<DragTarget>();
                    if (target.Type == DragTargetTypes.Card && target.TargetedCard.Owner == targetablePlayer) { return true; }
                }
            }
        }
        return false;
    }
}
//Yan