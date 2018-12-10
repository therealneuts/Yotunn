﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using System;
using Assets.Scripts.CardScript;

[Serializable]
public class Templar : Creature, IDamaging
    {
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
