using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cards;

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

    Cards.Creature creatureBeingDragged;

    CardAreas cardLocation;

    internal CardAreas CardLocation //Assigme à la variable CardLocation une
    {
        get
        {
            if (GetComponentInParent<HandLayout>() != null)
            {
                return CardAreas.Hand;
            }
            else if (GetComponentInParent<BattlegroundLayout> () != null)
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

        //Permet de se rendre à la méthode Attack de la créature utilisée.
        creatureBeingDragged = (Creature)cardBeingDragged.CardScript;
    }

    //Lorsque le joueur termine de tirer la carte, on détermine s'il choisi une cible légale.
    public override void OnEndDrag() 
    {
        if (DragSuccessful()) 
        {
            //Si la créature est dans la main, on la joue.
            if (CardLocation == CardAreas.Hand)
            {
                cardBeingDragged.Play();
            }
            //Si la créature est sur la table, elle attacque la cible.
            else if (CardLocation == CardAreas.Battleground)
            {
                
                CardManager targetCM = target.GetComponent<CardManager>();
                //Créature sur la table qui attack coute du mana, check si la reserve du joueur est assez haute pour attacker
                if(cardBeingDragged.Owner.hisManaReserve.NombreShardDispo >= cardBeingDragged.ManaCost)
                {
                    //Atack par la  méthode attack de la créature
                    creatureBeingDragged.Attack(targetCM);
                    //subtitue de la mana pooldu joueur le cout du mana
                    cardBeingDragged.Owner.hisManaReserve.NombreShardDispo -= cardBeingDragged.ManaCost;
                }
                transform.DOMove(v3PositionInitiale, duration);
            }
        }
        else
        {
            //DOMove change la position en fesant une transition à l'objet dans le jeu vers la position donnée au premier paramètre
            //à une vitesse donnée comme deuxième paramètre
            //.SetEase est une méthode qui est appelé pour dire comment la transition se fera
            transform.DOMove(v3PositionInitiale, duration);/*.SetEase(Ease.OutBounce, .5f, .1f); // <-- http://www.easings.net*/
        }
    }

    public override void OnDraggingInUpdate()
    {
        DrawLine();
        targets = SeekTargets();
    }

    //La méthode qui détermine si l'utilisateur a placé son curseur sur une cible légale pour la carte.
    protected override bool DragSuccessful()
    {
        //Si une créature est dans la main, la seule cible valide est le champ de son propriétaire
        if (CardLocation == CardAreas.Hand)
        {
            foreach(DragTarget dragTarget in targets)
            {
                if (dragTarget.Type == DragTargetTypes.Battleground  && dragTarget.Owner == cardBeingDragged.Owner) { return true; }
            }
        }
        //Si la créature est sur la table, les cibles légales sont les créatures ennemies.
        else if (CardLocation == CardAreas.Battleground)
        {
            foreach(DragTarget dragTarget in targets)
            {
                //Si le propriétaire de la cible est l'ennemi de cette carte...
                if (dragTarget.Owner == cardBeingDragged.Owner.Enemy)
                {
                    //La cible est assignée à target.
                    target = dragTarget;
                    return true;
                }
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
