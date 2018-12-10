using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Player : MonoBehaviour {



    public string m_Name { get; set; }
    HandLayout hand;

    CardManager _avatar;
    DeckBehavior _PlayerDeck;
    BattlegroundLayout _playerField = null;

    
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


    public BattlegroundLayout PlayerField
    {
        get
        {
            if (_playerField == null)
            {
                _playerField = GetComponentInChildren<BattlegroundLayout>();
            }
            return _playerField;
        }
    }

    public Player Enemy { get { return Battlefield.Players.GetOtherPlayer(this); } }

    public delegate void CardPlayedHandler(Player source, IPlayable carte);

    public event CardPlayedHandler CardPlayed;

    void Awake()
    {
        hand = GetComponentInChildren<HandLayout>();
        _PlayerDeck = GetComponentInChildren<DeckBehavior>();
    }

    public void Draw(int Num)
    {
        CardManager[] cards = _PlayerDeck.DrawMultiple(Num);

        foreach (CardManager card in cards)
        {
            hand.AddCardToHand(card);
        }
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
