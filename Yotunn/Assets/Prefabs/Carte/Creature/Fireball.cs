using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using Assets.Scripts.CardScript;

public class Fireball : Skill, IDamaging {


 
    public int Power
    {
        get
        {
            return cardManager.Power;
        }
    }

    public override void Play(CardManager target)
    {
        target.Health -= Power;

        //Crée une commande afin d'afficher une animation pour les joueurs
        Command cm = new CommandSkills(target);
    }

}

