using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command {

    //Une queue static pour que toute les objets qui hérite de command partage la queue
    public static Queue<Command> CommandQueue = new Queue<Command>();
    //Une booleen qui fait la fonction lock si une command est en procédure, laquelle est aussi partagé avec toutes les objet qui hérite de commande
    public static bool CommandIsExecuting;

    public delegate void CommandToExecute();

    //public Command(Func<> pCommandToExecute)
    //{

    //}

    //Ajout une commande à la queue
    public void AppendCommandToQueue()
    {
        //Ajoute la commande à la queue
        CommandQueue.Enqueue(this);
    }
    
    
}


