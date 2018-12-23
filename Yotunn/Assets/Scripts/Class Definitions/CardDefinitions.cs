using System;
using UnityEngine;

namespace Cards
{
    //namespace de Définition de Cartes et ses dérivés
    [Serializable()]
    public class Carte : MonoBehaviour
    {
        protected CardManager cardManager; //Crée une variable CardManager

        protected virtual void Start()
        {
            cardManager = GetComponent<CardManager>(); //Assigne à la variable cardmanager les cardManager individuels.
        }

    }
     public class Entity : Carte, IPermanent //Création de classe Entity, avec l'implémentation de l'interface IPermanent
    {

    }

    public class Boost : Carte, IPermanent, IPlayable //Création de la classe Boost, qui implémente l'interface IPermanent et IPlayable
    {
        public virtual void Play(CardManager target = null) { } //Implémente la fonction play de Iplayable.
    }

    public class Creature : Entity, IPlayable, IAttacker, IDamaging //Création de la classe Creature qui hérite de Entity. les créatures sont jouables, peuvent attaquer, et causer des dégâts.
    {
        public int Power //Variable power requise par l'interface IDamaging
        {
            get
            {
                return cardManager.Power; 
            }
        }

        public virtual void Attack(CardManager target) //Méthode Attack requise par l'interface IAttacking
        {
            target.Health -= Power;
        }
        public virtual void Play(CardManager target = null) //Fonction IPlayable requise par l'interface IPlayable
        {
            print(name + " played"); //Indique que la carte à été jouer dans la console
            cardManager.Owner.PlayerField.PlaceCardOnBattleground(cardManager); //Appelle la méthode PlaceCardonBattleground avec la carte comme paramètre.
        }
    }

    public class Avatar : Entity //Création de la classe Avatar, héritant de Entity
    {

    }

    public class Skill : Carte, IPlayable //Création de la classe Skill, héritant de la classe Carte et ayant l'interface IPlayable
    {
        public virtual void Play(CardManager target = null) { //Implémentation de la fonction Play requise par IPlayable

            cardManager.Discard();              //Les cartes dérivées appèlleront base.Play() à la fin de leur propre Play() pour se défausser.
        }
    }

}

