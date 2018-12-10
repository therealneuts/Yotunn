using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum CardAreas
{
    Hand,
    Battleground
}

public class CreatureDraggingBehavior : DraggingAction {


    [SerializeField] float duration = 0.2f;
    //Vector3 qui sauvegardera la position initiale du début du dragging
    private Vector3 v3PositionInitiale;
    DragTarget target;

    RaycastHit[] hits;
    List<DragTarget> targets = new List<DragTarget>();

    CardAreas cardLocation;

    internal CardAreas CardLocation
    {
        get
        {
            if (GetComponent<HandLayout>() != null)
            {
                return CardAreas.Hand;
            }
            else if (GetComponent<BattlegroundLayout> () != null)
            {
                return CardAreas.Battleground;
            }
            else
            {
                throw new System.Exception("cardLocation accessed while it's not in hand or on battlefield");
            }
        }
    }




    //Override qui sauvegarde la position de départ avant le début du dragging afin de le remettre à ça position initiale
    //Soit dans la main du joueur ou à l'endroit ou la carte était dans le jeu
    public override void OnStartDrag()
    {
        //transform qui nous indique la position de l'objet dans le plan du jeu
        v3PositionInitiale = this.transform.position;
    }

    //Lorsque le joueur termine de tirer la carte, on détermine s'il choisi une cible légale.
    public override void OnEndDrag()
    {
        bool targetIsLegal = DragSuccessful();
        if (targetIsLegal)
        {
            print("Dragged over a legal target!");
            CardManager targetCard = target.GetComponent<CardManager>();
            cardBeingDragged.Play(targetCard);
        }
        else { print("Dragged to an illegal target!"); }
        //DOMove change la position en fesant une transition à l'objet dans le jeu vers la position donnée au premier paramètre
        //à une vitesse donnée comme deuxième paramètre
        //.SetEase est une méthode qui est appelé pour dire comment la transition se fera
        transform.DOMove(v3PositionInitiale, duration);/*.SetEase(Ease.OutBounce, .5f, .1f); // <-- http://www.easings.net*/
    }

    public override void OnDraggingInUpdate()
    {
        DrawLine();
    }

    //La méthode qui détermine si l'utilisateur a placé son curseur sur une cible légale pour la carte.
    protected override bool DragSuccessful()
    {
        //Si une créature est 
        if (CardLocation == CardAreas.Hand)
        {
            foreach(DragTarget target in targets)
            {
                if (target.Type == DragTargetTypes.Battleground  && target.TargetedPlayerArea) { return true; }
            }
        }
        //Si aucune cible légale n'est sous le curseur, retourne faux.
        return false;
    }

    //à chaque cycle, vérifie si une cible se trouve sous le curseur.
    protected List<DragTarget> SeekTargets()
    {
        targets.Clear();

        hits = Physics.RaycastAll(ray: Camera.main.ScreenPointToRay(Input.mousePosition),
                            maxDistance: 30f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<DragTarget>() != null)
            {
                targets.Add(hit.collider.GetComponent<DragTarget>());
            }
        }

        return targets;
    }
}
