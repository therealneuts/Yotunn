using System;
using UnityEngine;

namespace Cards
{
    abstract class Carte : MonoBehaviour
    {
        [SerializeField] protected string m_Name;
        [SerializeField] protected string m_RulesText;
        protected Player _owner;
        protected Player _controller;

        public Player Owner
        {
            get { return _owner; }
        }

        public Player Controller
        {
            get { return _controller; }
            set { _controller = value; }
        }

        protected Player Enemy
        {
            get
            {
                 return Controller.Enemy;
            }
        }

        public Carte(Player owner)
        {
            _owner = owner;
            _controller = owner;
        }
    }
    abstract class Entity : Carte, IPermanent
    {
        protected int _hitPoints;
        protected int _power;
        protected int _armor;

        public Entity(Player owner) : base(owner)
        {

        }

        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                if (_hitPoints <= 0)
                {
                    Destroy();
                }
            }
        }
        public int Power { get; set; }
        public int Armor { get; set; }

        public abstract void Destroy();
    }

    abstract class Boost : Carte, IPermanent, IPlayable
    {
        public Boost(Player owner) : base(owner)
        {

        }

        public abstract int Cost { get; }

        public abstract void Destroy();
        public abstract void Play();
    }

    abstract class Creature : Entity, IPlayable, IAttacker
    {
        public Creature(Player owner) : base(owner)
        {
        }

        public abstract int Cost { get; }

        public abstract void Attack();
        public abstract void Play();
    }

    abstract class Avatar : Entity
    {
        public Avatar(Player owner) : base(owner)
        {
        }
    }

    abstract class Skill : Carte, IPlayable
    {
        public Skill(Player owner) : base(owner)
        {

        }

        public abstract int Cost { get; }

        public abstract void Play();
    }

}

