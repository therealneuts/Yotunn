using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using Assets.Scripts.Registration;

public class ScoreBoard : MonoBehaviour {

    public Text ScoreBoardText;

	// Use this for initialization
	void Start () {

        string Afficher = "";


        string path = Environment.CurrentDirectory + @"\Assets\BaseDonnéeVirtuel\Players.txt";

        //Ouverture du file stream avec le mode de open ou create
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        string line;
        // Parcours les lignes jusqu'a ce qu'il arrive à la fin
        while ((line = sr.ReadLine()) != null)
        {
            //Split la ligne ou il y a le char ',' afin de savoir la séparation nom et Number of win
            string[] stSplit = line.Split((char)',');
            Afficher += stSplit[0] + "  " + stSplit[1] + Environment.NewLine;
        }

        //fermer le fs
        fs.Close();

        ScoreBoardText.text = Afficher;

    }

}
