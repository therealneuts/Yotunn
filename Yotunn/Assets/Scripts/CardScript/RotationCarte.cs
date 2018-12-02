using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Dérive de la class Monobehavior, 
// Nous permet d'accéder à des méthodes importantes dans le code:
// Start(), Update(), FixedUpdate(), LateUpdate() --> sont des méthodes qui sont appelé dès l'instanciation de l'objet
//                  --> Les fonctions avec le nom Update se font appeller indifiniment par le mécanisme du jeu lorsque l'objet est instancié 
// Source pour documentation sur la class parent monobehavior https://docs.unity3d.com/ScriptReference/MonoBehaviour.html 


[ExecuteInEditMode]
public class RotationCarte : MonoBehaviour {
    // Ce script est attaché à la carte pour afficher laquelles des faces de la carte devant être affiché.

    // Référence à l'avant de la carte.
    // Alex C: Dans Unity, les propriétés publiques des objets peuvent être assignées et modifiées dans l'éditeur visuel.
    public RectTransform CarteFace;

    // Référence à l'arrière de la carte.
    public RectTransform CarteBack;

    //Le FacePoint se situe une courte distance à l'avant de la carte.
    public Transform FacePoint;

    //Une boite qui représente l'aspect physique de la carte -- peut reconnaître des collisions.
    Collider collider;

    //La position de la caméra.
    Transform cameraTransform;

    enum CardFaces { Front, Back};

    CardFaces currentFace = CardFaces.Front;

    private void Start()
    {
        //Assignation des références.
        collider = GetComponent<Collider>();                        //Méthode de Unity qui trouve et rapporte un Collider.
        cameraTransform = Camera.main.transform;
    }

    // Méthode appelée à chaque changement d'image (frame).
    void Update () {
        //Par défaut, on montre le devant.
        currentFace = CardFaces.Front;

        //Vecteur entre la carte et la caméra.
        Vector3 vectorToCard = FacePoint.position - cameraTransform.position;

        //Raycast produit un rayon avec une origine, une direction et une distance maximale précises.
        RaycastHit[] hits;

        //Le rayon a pour origine la caméra, pour direction le vecteur entre la caméra et le FacePoint, et pour distance maximale,
        //la magnitude de ce dernier. Plus simplement, c'est un rayon entre la caméra et le FacePoint.
        hits = Physics.RaycastAll(  origin: cameraTransform.position, 
                                    direction: vectorToCard,
                                    maxDistance: vectorToCard.magnitude);

        //Puisque la distance maximale est la distance entre la caméra et le FacePoint:
        //1 - Si la carte fait face à la caméra, il n'y aura pas de collision avec le collider.
        //2 - Autrement, il y aura une collision avec le collider en chemin vers le Facepoint.

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider == collider)
            {
                currentFace = CardFaces.Back;
            }
        }


        ToggleDisplay();
    }

    //Détermine la face de la carte qui sera visible.
    private void ToggleDisplay()
    {
        switch (currentFace)
        {
            case CardFaces.Front:
                CarteFace.gameObject.SetActive(true);
                CarteBack.gameObject.SetActive(false);
                break;
            case CardFaces.Back:
                CarteFace.gameObject.SetActive(false);
                CarteBack.gameObject.SetActive(true);
                break;
            default:
                throw new System.Exception("Unexpected CardFace");
        }
    }
    //Yan, Alex C
}
