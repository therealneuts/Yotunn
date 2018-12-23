using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerZones { Bottom, Top }

public class PlayerArea : MonoBehaviour {

    public PlayerZones zone;
    Player _owner;
    HandLayout handlayout;

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
        Owner = GetComponent<Player>();
        handlayout = GetComponentInChildren<HandLayout>();
	}
}
