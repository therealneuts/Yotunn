using System;
using UnityEngine;

namespace Cards
{
    [Serializable()]
    public class Carte : MonoBehaviour
    {
        protected CardManager cardManager;

        protected virtual void Start()
        {
            cardManager = GetComponent<CardManager>();
        }

    }
     public class Entity : Carte, IPermanent
    {

    }

    public class Boost : Carte, IPermanent, IPlayable
    {
        public virtual void Play(CardManager target = null) { }
    }

    public class Creature : Entity, IPlayable, IAttacker, IDamaging
    {
        public int Power
        {
            get
            {
                return cardManager.Power;
            }
        }

        public virtual void Attack(CardManager target)
        {
            target.Health -= Power;
        }
        public virtual void Play(CardManager target = null)
        {
            print(name + " played");
            cardManager.Owner.PlayerField.PlaceCardOnBattleground(cardManager);
        }
    }

    public class Avatar : Entity
    {

    }

    public class Skill : Carte, IPlayable
    {
        public virtual void Play(CardManager target = null) { }
    }

}

