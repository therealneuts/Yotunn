using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using System;

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

}
