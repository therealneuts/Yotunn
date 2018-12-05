using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

    [Header("Players")]
    public Player player1;
    public Player player2;

    [Header("PlayersTags")]
    public Text player_tag1;
    public Text player_tag2;

    [Header("Prefabs")]
    public GameObject cardPrefab;

    [Header("Timings")]
    public float cardTransitionTime = 0.5f;
    public float cardPreviewTime = 1f;

    //Dictionnaire pour les joueurs
    Dictionary<PlayerZones, Player> players = new Dictionary<PlayerZones, Player>();

    public static GlobalSettings instance;

	// Use this for initialization
	void Awake () {
        //Nom pour les joueur
        player1.m_Name = GroundStats.st_Player1;
        player2.m_Name = GroundStats.st_Player2;

        //Dans les tags
        player_tag1.text = player1.m_Name;
        player_tag2.text = player2.m_Name;

        //Ajout des player dans un dictionnaire comme clé nous avons le PlayerZones
        players.Add(PlayerZones.Bottom, player1);
        players.Add(PlayerZones.Top, player2);
        instance = this;
	}
	
    
}
