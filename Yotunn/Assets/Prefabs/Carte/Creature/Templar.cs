using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using System;

[Serializable]
public class Templar : Creature, IDamaging
    {

	protected override void Start()
    {
        base.Start();
        cardManager.Power++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
