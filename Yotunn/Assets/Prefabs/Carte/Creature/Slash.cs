﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Slash : Skill, IDamaging {
    public int Power
    {
        get
        {
            return cardManager.Power; ;
        }
    }

    public override void Play(CardManager target)
    {
        target.Health -= Power;
    }
}