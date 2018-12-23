using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Prend en charge le tirage et le ciblage des cartes abilités.
/// </summary>
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
        //Les cibles légales de la carte qu'on tire.
        TargetingOptions = cardBeingDragged.cardAsset.Targets;

        //Assigne le joueur qui peut être ciblé par cette carte.
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
    
    //Lorsque le joueur termine de tirer la carte, on détermine s'il choisi une cible légale.
    public override void OnEndDrag()
    {
        if (cardBeingDragged.Owner.hisManaReserve.NombreShardDispo >= cardBeingDragged.ManaCost) //Vérifie que le joueur à le mana disponible pour jouer la carte.
        {
            if (DragSuccessful())
            {
                CardManager targetCard = target.GetComponent<CardManager>();
                cardBeingDragged.Owner.hisManaReserve.NombreShardDispo -= cardBeingDragged.ManaCost; //Soustrait le coût de la carte à la réserve de mana du joueur.
                cardBeingDragged.Play(targetCard);
            }
            else
            {
                transform.DOMove(v3PositionInitiale, duration);/*.SetEase(Ease.OutBounce, .5f, .1f); // <-- http://www.easings.net*/
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

        //à chaque cycle, vérifie si une cible se trouve sous le curseur.
        hits = Physics.RaycastAll(  ray: Camera.main.ScreenPointToRay(Input.mousePosition),
                                    maxDistance: 30f    );
    }

    //La méthode qui détermine si l'utilisateur a placé son curseur sur une cible légale pour la carte.
    protected override bool DragSuccessful()
    {
        //TargetingOptions.NoTarget ne prend pas de cible spécifique, alors on vérifie seulement si le joueur l'a tirée sur l'aire de jeu.
        if (TargetingOptions == TargetingOptions.NoTarget)
        {
            foreach (RaycastHit hit in hits)
            {
                DragTarget dragTarget = hit.collider.GetComponent<DragTarget>();

                if (dragTarget != null)
                {
                    if (dragTarget.Type == DragTargetTypes.Battleground) { return true; }
                }
            }
        }
        //Sinon, on vérifie si la carte ciblée est légale.
        else
        {
            //Deux options nous est offert ici soit la carte qui a activé la carte ou le collider d'un objet sur le plan de jeu
            foreach (RaycastHit hit in hits)
            {
                //Si celui qui a hit est une carte 
                if (hit.collider.GetComponent<DragTarget>() != null)
                {
                    target = hit.collider.GetComponent<DragTarget>();


                    if (target.Type == DragTargetTypes.Card && targetablePlayer == null) { return true; }
                    if (target.Type == DragTargetTypes.Card && target.TargetedCard.Owner == targetablePlayer) { return true; }
                }
                //Sinon c'est le deuxième choix c'est le portrait qui est attaché auparent qui contient un player lequel est la personne que nous vooulons attaquer
                else if(hit.collider.GetComponentInParent<Player>() != null)
                {
                    Player pHited = hit.collider.GetComponentInParent<Player>();
                    //Todo add player if not player current
                    pHited.MaxHealth -= cardBeingDragged.Power;

                    cardBeingDragged.Owner.hisManaReserve.NombreShardDispo -= cardBeingDragged.ManaCost; //Soustrait le coût de la carte à la réserve de mana du joueur.
                    cardBeingDragged.Discard();
                    return false;
                }
            }
        }
        //Si aucune cible légale n'est sous le curseur, retourne faux.
        return false;
    }
}
//Yan, Alex C