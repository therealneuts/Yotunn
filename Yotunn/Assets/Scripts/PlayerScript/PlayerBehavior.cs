using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class PlayerBehavior : MonoBehaviour {

    Player CurPlayer;
    GameController gc = FindObjectOfType<GameController>();
    private void GetCurrentPlayer()
    {
        CurPlayer = gc.CurrentPlayer;
    }



    
   


   
}
