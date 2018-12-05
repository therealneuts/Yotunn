using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class GlobalSettings : MonoBehaviour {

    [Header("Players")]
    public Player player1;
    public Player player2;

    [Header("Prefabs")]
    public GameObject cardPrefab;

    [Header("Timings")]
    public float cardTransitionTime = 0.5f;
    public float cardPreviewTime = 1f;

    Dictionary<PlayerZones, Player> players = new Dictionary<PlayerZones, Player>();

    public static GlobalSettings instance;

	// Use this for initialization
	void Awake () {
        players.Add(PlayerZones.Bottom, player1);
        players.Add(PlayerZones.Top, player2);
        instance = this;
	}
	
    
}
