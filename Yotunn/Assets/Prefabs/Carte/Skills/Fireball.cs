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

        Command cm = new CommandSkills(target, Power);
        base.Play();
        
    }

    

}

