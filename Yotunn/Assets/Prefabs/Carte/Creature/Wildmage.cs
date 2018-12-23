using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Wildmage : Creature, IHasStartTurnAction
{
    public TurnFlowEventTargeting Targeting
    {
        get
        {
            return TurnFlowEventTargeting.Self;
        }
    }

    //Si c'est le tour du propriétaire de cete carte, elle gagne 1 de puissance.
    public void OnStartTurn(Player currentPlayer)
    {
        if (currentPlayer == cardManager.Owner)
        {
            cardManager.Power++;
        }
    }
}
