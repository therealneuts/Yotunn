using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    public interface IDamaging
    {
        int Power { get;}
    }

    public enum TurnFlowEventTargeting
    {
        Self,
        Enemy,
        Both
    }

    public interface IHasEndStepAction
    {
        TurnFlowEventTargeting Targeting { get; }

        void OnEndStep(Player currentPlayer);    }

    public interface IHasStartTurnAction
    {
        TurnFlowEventTargeting Targeting { get; }

        void OnStartTurn(Player currentPlayer);
    }

    public interface IDefensive
    {
        int BaseArmor { get; }
        int Armor { get; set; }
    }

    enum TargetMode { Ally, Enemy, Both }

    public interface ITargeted
    {
        System.Type TargetType { get; }
        int TargetNum { get; }
        bool TargetPredicate(Carte carte);

        List<CardManager> LegalTargets { get; }

        List<CardManager> SelectTargets();


    }

    public interface IAttacker
    {
        void Attack(CardManager target);
    }

    public interface IPlayable
    {
        void Play(CardManager target = null);
    }

    public interface IPermanent
    {
        //todo position
    }
}