using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    //Référence pour le text qui dit à qui est le tour
    [Header("Mettre la référence du texte message ici")]
    public Text TextMessage;
    //référence vers l'objet du canvas afin de le désactiver
    public GameObject MessageCanvas;

    //Afin de créer un singleton pour ne pas en créer plus qu'un
    public static Message Instance;


    private void Start()
    {
        //La variable Instance qui pointe vers la seul instance du MessageManager
        Instance = this;
        //Désactivation de l'objet MessageCanvas dans le jeu
        MessageCanvas.SetActive(false);        
    }

    public void ShowTurnMessage(Player pPlayer)
    {
        //Demande à l'instance de l'objet Message d'afficher le message du player avec un wait time donné 
        string Message = "Tour a " + pPlayer.m_Name;

        //Commence un commande dans le background
        StartCoroutine(ShowMessageCoroutine(Message, 2f));
    }

    public void ShowGameWinner(Player PlayerWinner)
    {
        //Demande à l'instance de l'objet Message d'afficher le message du player avec un wait time donné 
        string Message = "the winner IS " + PlayerWinner.m_Name;

        //Commence un commande dans le background
        StartCoroutine(ShowMessageCoroutine(Message, 9f));

    }

    IEnumerator ShowMessageCoroutine(string Message, float duration)
    {
        //Passe le text au text dans le jeu
        TextMessage.text = Message;
        //Activation de l'objet MessageCanvas 
        MessageCanvas.SetActive(true);

        //Attend pour quelque seconde
        yield return new WaitForSeconds(duration);

        //Désactive le gameObject
        MessageCanvas.SetActive(false);
        
    }
}
//Yan
