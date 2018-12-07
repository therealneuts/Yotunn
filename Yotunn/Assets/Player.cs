using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Player : MonoBehaviour {



    public string m_Name { get; set; }
    List<CardManager> m_Hand { get; set; }

    CardManager _avatar;
    DeckBehavior _PlayerDeck;

    
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

   
    

    public Player Enemy { get { return Battlefield.Players.GetOtherPlayer(this); } }


    public delegate void CardPlayedHandler(Player source, IPlayable carte);

    public event CardPlayedHandler CardPlayed;

    void Awake()
    {
        _PlayerDeck = GetComponentInChildren<DeckBehavior>();
    }

    public void Draw(int Num)
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
