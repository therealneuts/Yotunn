using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Draggable : MonoBehaviour {

    //Afin de comprendre le code:
    //Un gameObject est l'objet auquel le composant est attaché, ex: DraggingBehavior est attaché a une carte, 
    //le composant, est toujours attaché à un objet du jeu.
    //Un gameObject a une relation avec un objet de type Component, laquelle est composé de d'une référence avec le gameObject auquel le composant est attaché
    //et transform, lequel sera utilisé dans le code pour savoir la position de l'objet dasns le plan de jeu
    

    // Référence vers un script de type DraggingAction
    private DraggingAction anDraggable;

    // Nous informe si nous sommes entrain de bouger cet objet
    private bool boDragging = false;

    // Nous indique la sitance du game object au pointeur lors du click
    private Vector3 v3Cursor = Vector3.zero;

    // Indique le Z en coordonné dans le plan de jeu du curseur par rapport à la vue de la camera de la scene
    private float flIndicZ;

    Vector3 draggingOffset;

    // Méthode de monobehavior
    // Cette méthode est appelé une seul fois pendant la duré de vie de l'instance de script
    // Une fois que tous les objets qui composent le gameObject de unity soit initialisés
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    private void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
        // GetComponent retourne un objet du type demandé à partir de la liste des objets qui composent un ensemble d'objet dans unity
        anDraggable = GetComponent<DraggingAction>();
    }

    // Méthode de Monobehavior qui est appelé lorsque nous appuyons sur le boutton de la sourie par-dessus le gamObject
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rch;

        // Si le CanDrag est vrai, cela voudrait dire que nous pouvons réaliser les actions de bouger le gameObject
        if (anDraggable != null && anDraggable.CanDrag)
        {
            if (Physics.Raycast(ray, out rch))
            {
                draggingOffset = transform.position - rch.point;

            }
            // Flag a l'object que nous sommes entrain de bouger l'objet
            boDragging = true;
            
            // Call le début de bougemant d'objet
            anDraggable.OnStartDrag();

            // Camera.main indique la première camera du tag MainCamera dans le jeu 
            // cette indicateur nous aidera pour trouver la position exact du curseur dans le jeu
            flIndicZ = -Camera.main.transform.position.z + transform.position.z;
        }
    }

    // Méthode de Monobehavior qui est appelé à chaque (frame)
    private void FixedUpdate()
    {
        if (boDragging)
        {
            // Retourne la valeur de la position du curseur
            var CurseurPositionCamera = Input.mousePosition;

            // Change la position Z de la variable crée afin de crée une position réel pour le déplacement selon l'indicateur 
            CurseurPositionCamera.z = flIndicZ;

            // Représente un point en 3 dimension qui sauvegarde les coordonnés de la sourie selon la vue de la camera
            Vector3 v3PositionSourie = Camera.main.ScreenToWorldPoint(CurseurPositionCamera);
            // Appelé pour performer à chaque frame 
            anDraggable.OnDraggingInUpdate();
            // transform est une propriété pour toute game object (voir haut)
            // nous updastons la position de l'objet selon un nouveau vecteur crée selon la position du curseur
            Vector3 newPos = v3PositionSourie + draggingOffset;
            transform.position = newPos;
        }
    }

    // Méthode de Monobehavior qui est appelé lorsque l'événement de mouse release est fait
    private void OnMouseUp()
    {
        // la bool est true la rendre false
        if (boDragging)
        {
            boDragging = false;
            //Appelle la méthode de terminaison de bougemant de carte
            anDraggable.OnEndDrag();
        }
    }

    private Vector3 MousePosToWorldSpace()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}//Yan
