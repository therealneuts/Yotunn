using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Player : MonoBehaviour {


    public string m_Name { get; set; }
    Hand m_Hand { get; set; }

    CardManager _avatar;

    public CardManager Avatar
    {
        get
        {
            return _avatar;
        }

        set
        {
            _avatar = value;
        }
    }

    private Deck _deck;

    public Deck Deck
    {
        get { return _deck; }
    }

    public Player Enemy { get { return Battlefield.Players.GetOtherPlayer(this); } }


    public delegate void CardPlayedHandler(Player source, IPlayable carte);

    public event CardPlayedHandler CardPlayed;


    public void Draw(int Pige)
    {

    }

    public void Play(IPlayable card)
    {
        card.Play();
        if (CardPlayed != null)
        {
            CardPlayed(this, card);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
