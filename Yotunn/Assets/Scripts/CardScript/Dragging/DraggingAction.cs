using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe abstraite qui définira ce qu'un objet dérivé de la classe abstraite doit implimenter
public abstract class DraggingAction : MonoBehaviour {
    // Méthode qui devra être appellé lorsque le joueur essait de bouger la carte
    public abstract void OnStartDrag();

    // Méthode qui devra être appellé lorsque le joueur lache le boutton de la sourie  
    public abstract void OnEndDrag();

    // Méthode qui est appellé continuellement lorsque le joueur bouge la carte
    public abstract void OnDraggingInUpdate();

    // Champ qui défini si on peut bouger la carte
    public virtual bool CanDrag
    {
        get
        {
            return true;
        }
    }

    // Méthode qui retourne une bool qui nous dira si la carte que nous avons essayé de bougé à réussi à atteindre la destination
    protected abstract bool DragSuccessful();
}

