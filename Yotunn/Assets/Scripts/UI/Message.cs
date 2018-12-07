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

    private void Awake()
    {
        //La variable Instance qui pointe vers la seul instance du MessageManager
        Instance = this;
        //Désactivation de l'objet MessageCanvas dans le jeu
        MessageCanvas.SetActive(false);
    }

    public void ShowMessage(string Message, float duration)
    {
        //Commence un commande dans le background
        StartCoroutine(ShowMessageCoroutine(Message, duration));
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
