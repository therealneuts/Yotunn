using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragTargetTypes
{
    Card,
    Battleground
}
    
public class DragTarget : MonoBehaviour {

    DragTargetTypes _type;
    CardManager _targetedCard;
    Player _owner;

    public DragTargetTypes Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    public CardManager TargetedCard
    {
        get
        {
            return _targetedCard;
        }

        set
        {
            _targetedCard = value;
        }
    }

    public Player Owner
    {
        get
        {
            return _owner;
        }

        set
        {
            _owner = value;
        }
    }

    // Use this for initialization
    void Start () {
		if (GetComponentInParent<CardManager>() != null)
        {
            Type = DragTargetTypes.Card;
            TargetedCard = GetComponent<CardManager>();
        }
        else
        {
            Type = DragTargetTypes.Battleground;
            Owner = GetComponentInParent<Player>();
        }

	}

}
