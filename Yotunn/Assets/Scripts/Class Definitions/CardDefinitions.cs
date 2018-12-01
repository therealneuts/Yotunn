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
        public virtual void Play() { }
    }

    public class Creature : Entity, IPlayable, IAttacker
    {
        public virtual void Attack() { }
        public virtual void Play() { }
    }

    public class Avatar : Entity
    {

    }

    public class Skill : Carte, IPlayable
    {
        public virtual void Play() { }
    }

}

