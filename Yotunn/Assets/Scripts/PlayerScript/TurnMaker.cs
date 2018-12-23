using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public abstract class TurnMaker : MonoBehaviour {

    internal Player p;

    void Awake()
    {        
    }

    public virtual void OnUpkeep()
    {   
    }
}
