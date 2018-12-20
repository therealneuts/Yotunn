using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.UI;

public class Player : MonoBehaviour {



    public string m_Name { get; set; }
    HandLayout hand;

    CardManager _avatar;
    DeckBehavior _PlayerDeck;
    BattlegroundLayout _playerField = null;

    public ReserveMana hisManaReserve;

    protected int _MaxHealth = 20;
    public Text HealthText;

    public int MaxHealth
    {
        get
        {
            return _MaxHealth;
        }

        set
        {
            _MaxHealth = value;
            HealthText.text = _MaxHealth.ToString();
            if(_MaxHealth <= 0)
            {
                Message.Instance.ShowGameWinner(this.Enemy);
            }
        }

    }


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
        HealthText.text = _MaxHealth.ToString();
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
