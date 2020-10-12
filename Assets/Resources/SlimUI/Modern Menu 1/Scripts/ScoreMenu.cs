using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{    
    void Start()
    {
        if (!PlayerPrefs.HasKey("Scores"))
        {
            PlayerPrefs.SetString("Scores", CreateDefaultScores());
            new Score().AddNewScore(new Score("El Fari", DateTime.Now, 1));
        };
        UpdateScoresPanel();       
    }
    public void UpdateScoresPanel()
    {
        GameObject[] rankPanels = GameObject.FindGameObjectsWithTag("ScoreRank");
        List<Score> scores = new Score().GetAllScores();
        for (int i = 0; i < rankPanels.Length; i++)
        {
            TMPro.TextMeshProUGUI[] texts = rankPanels[i].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
            texts[0].SetText(scores[i].player);
            texts[1].SetText(scores[i].date);
            texts[2].SetText(scores[i].score);
        }
    }
    private string CreateDefaultScores()
    {
        String scores = String.Empty;
        scores += CreateScore("El Fari", DateTime.Now, 0);
        scores += CreateScore("The Invictus", DateTime.Now, 9995);
        scores += CreateScore("Moncho", DateTime.Now, 10000);
        scores += CreateScore("Stradmann", DateTime.Now, 9998);
        scores += CreateScore("RAlonso", DateTime.Now, 9999);
        scores += CreateScore("Sweet", DateTime.Now, 9994);
        scores += CreateScore("__ProPlAy€r__#1", DateTime.Now, 9993);
        scores += CreateScore("Ipso", DateTime.Now, 9997);
        scores += CreateScore("Paco is back", DateTime.Now, 9992);
        return scores;
    }

    public string CreateScore(string playerName, DateTime date, int score)
    {
        string dateString = date.ToString("dd-MM-yyy HH:mm");
        string scoreString = score.ToString("n0");
        return playerName + ";" + dateString + ";" + scoreString + ";";
    }    
}
