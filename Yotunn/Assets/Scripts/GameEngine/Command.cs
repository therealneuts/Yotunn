using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command{

    //Une queue static pour que toute les objets qui hérite de command partage la queue
    public static Queue<Command> CommandQueue = new Queue<Command>();
    //Une booleen qui fait la fonction lock si une command est en procédure, laquelle est aussi partagé avec toutes les objet qui hérite de commande
    public static bool CommandIsExecuting = false;

    public virtual void StartCommandExecution()
    {
        //Cette méthode qui doit être override fait tout ce que le typ de carte doit faire
    } 

    //Ajout une commande à la queue
    public void AppendCommandToQueue()
    {
        //Ajoute la commande à la queue
        CommandQueue.Enqueue(this);
        //Si une commande n'est pas en exécution de faire la commande qui est dans la queue
        if (!CommandIsExecuting)
            StartFirstCommandInQueue();

    }
    public static void StartFirstCommandInQueue()
    {
        //Dire qu'une commande est en cours d'exécution
        CommandIsExecuting = true;
        //Enlève la première dans la ligne est exécute la commande associé à celle-ci
        CommandQueue.Dequeue().StartCommandExecution();
    }
    
}
//Yan 

