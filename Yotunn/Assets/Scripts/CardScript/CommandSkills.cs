using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;

namespace Assets.Scripts.CardScript
{
    class CommandSkills : Command
    {
        //Référence du cardManager de la carte qui a été touché
        CardManager Targeted;
        int powerForce;

        //Constructeur qui prend en paramètre le CardManager
        public CommandSkills(CardManager target, int power)
        {
            //pointer la variable vers le target qui à été touché
            Targeted = target;
            powerForce = power;
            //Ajouter cette instance de commande a la queue
            //laquelle exécute la commande 
            AppendCommandToQueue();
        }

        public override void StartCommandExecution()
        {
            //shake l'objet du target avec une fonction de tweening
            Targeted.GetComponent<CartePreview>().GUIhasBeenAttacked(powerForce);
            //Après l'excution de remettre la commandIsExecuting en false afin de libérer les prochaines commandes
            CommandIsExecuting = false;
        }
    }
}
