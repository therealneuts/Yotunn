using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    interface IDamaging
    {

    }

    enum TurnFlowEventTargeting
    {
        Self,
        Enemy,
        Both
    }

    interface IHasEndStepAction
    {
        TurnFlowEventTargeting Targeting { get; }

        void OnEndStep(Player currentPlayer);    }

    interface IHasStartTurnAction
    {
        TurnFlowEventTargeting Targeting { get; }

        void OnStartTurn(Player currentPlayer);
    }

    interface IDefensive
    {
        int BaseArmor { get; }
        int Armor { get; set; }
    }

    enum TargetMode { Ally, Enemy, Both }

    interface ITargeted
    {
        System.Type TargetType { get; }
        int TargetNum { get; }
        bool TargetPredicate(Carte carte);

        Hand LegalTargets { get; }

        Hand SelectTargets();


    }

    interface IAttacker
    {
        void Attack();
    }

    interface IPlayable
    {
        void Play();
    }

    interface IPermanent
    {
        //todo position
    }
}