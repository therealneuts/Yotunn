using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.IO;

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
        if(Player2.text != "" && Player1.text != "")
        {
            st_Player1 = Player1.text;
            st_Player2 = Player2.text;

            string path = Environment.CurrentDirectory + @"\Assets\BaseDonnéeVirtuel\Players.txt";

            bool boPlayer1exists =false;
            bool boPlayer2exists = false ;

            //Ouverture du file stream avec le mode de open ou create
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

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

            if (!boPlayer1exists)
                sw.WriteLine(st_Player1 + ",0");
            if (!boPlayer2exists)
                sw.WriteLine(st_Player2 + ",0");

            sw.Flush(); //HERE

            //fermer le fs
            fs.Close();

            Player1.text = "";
            Player2.text = "";
        }
    }

}
