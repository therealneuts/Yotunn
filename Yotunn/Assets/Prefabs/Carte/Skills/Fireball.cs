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
        //Création d'une commande avec la target et le power en paramètre
        Command cm = new CommandSkills(target, Power);
        base.Play();        
    }

    

}

