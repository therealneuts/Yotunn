using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class GameController : MonoBehaviour {

    internal delegate void UpkeepHandler(Player currentPlayer);
    internal event UpkeepHandler Upkeep;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
