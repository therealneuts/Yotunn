using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using UnityEngine.SceneManagement;

public static class GroundStats
{
    public static string st_Player1;
    public static string st_Player2;
}

public class RegistrationJoueur : MonoBehaviour {

    //Les inputsfield dans le jeu dans lesquels nous retrouverons le text rentré par le joueur
    public InputField Player1;
    public InputField Player2;

    //Les variables que nous stockerons le text mis dans le inputField
    private string st_Player1;
    private string st_Player2;

    //La fonction quii sera appelé au bouton click
    public void buttonClick_Jouer()
    {
        //Seulement si les chaînes ne sont pas vides
        if(Player2.text != "" && Player1.text != "")
        {
            st_Player1 = Player1.text;
            st_Player2 = Player2.text;

            //bool qui vérifie s'il n'a pas eu d'erreur
            bool hasError = false;

            //Construction de la chaine de connection
            string path = Environment.CurrentDirectory + @"\Assets\BaseDonnéeVirtuel\Players.txt";

            //Bool qui regarde si un ou les joueurs existent déjà
            bool boPlayer1exists =false;
            bool boPlayer2exists = false ;

            //Déclaration du fileStream
            FileStream fs = null;

            try
            {
                //Ouverture du file stream avec le mode de open ou create
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                //Instanciation d'un nouveau StreamReader et Writer
                StreamReader sr = new StreamReader(fs);
                StreamWriter sw = new StreamWriter(fs);

                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(st_Player1))
                        boPlayer1exists = true;
                    if (line.Contains(st_Player2))
                        boPlayer2exists = true;
                }

                //Si la bool est False aujouter les string dans le fichier avec un score de zéro
                if (!boPlayer1exists)
                    sw.WriteLine(st_Player1 + ",0");
                if (!boPlayer2exists)
                    sw.WriteLine(st_Player2 + ",0");

                sw.Flush(); //De toute écrire ce qui n'a pas été écrit

            }catch(Exception ex)
            {
                print("Une erreur a survenu : " + ex.Message);
                hasError = true;
            }
            finally
            {
                //fermer le fs
                fs.Close();
            }

            //Clear le text
            Player1.text = "";
            Player2.text = "";

            if (!hasError)
            {
                GroundStats.st_Player1 = this.st_Player1;
                GroundStats.st_Player2 = this.st_Player2;

                //Load la scene de battle ground
                SceneManager.LoadScene(1);
                SceneManager.UnloadSceneAsync(0);
            }
        }
    }


}
