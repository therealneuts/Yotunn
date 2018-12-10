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

        //Constructeur qui prend en paramètre le CardManager
        public CommandSkills(CardManager target)
        {
            //pointer la variable vers le target qui à été touché
            Targeted = target;
            //Ajouter cette instance de commande a la queue
            //laquelle exécute la commande 
            AppendCommandToQueue();
        }

        public override void StartCommandExecution()
        {
            //shake l'objet du target avec une fonction de tweening
            Targeted.transform.DOShakePosition(2f, 1, 10, 90, false, true);
            //Après l'excution de remettre la commandIsExecuting en false afin de libérer les prochaines commandes
            CommandIsExecuting = false;

        }
    }
}
