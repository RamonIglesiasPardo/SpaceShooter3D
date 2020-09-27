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
        scores += CreateScore("El Fari", DateTime.Now, 9996);
        scores += CreateScore("The Invictus", DateTime.Now, 9995);
        scores += CreateScore("Moncho", DateTime.Now, 10000);
        scores += CreateScore("Stradmann", DateTime.Now, 9999);
        scores += CreateScore("RAlonso", DateTime.Now, 9998);
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

    public class Score
    {
        public string player;
        public string date;
        public string score;
        public int scoreAsInt;
        public Score()
        {
            //TODO Default constructor
        }
        public Score(string playerName, DateTime date, int score)
        {
            this.player = playerName;
            this.date = date.ToString("dd-MM-yyy HH:mm");
            this.score = score.ToString("n0");
            this.scoreAsInt = score;
        }
        public Score(string playerName, string date, string score)
        {
            this.player = playerName;
            this.date = date;
            this.score = score;
            foreach (var c in new string[] { "." })
            {
                score = score.Replace(c, string.Empty);
            }
            this.scoreAsInt = int.Parse(score);
        }
        public string CreateScore(string playerName, string date, string score)
        {
            return playerName + ";" + date + ";" + score + ";";
        }
        public List<Score> GetAllScores()
        {
            List<Score> scores = new List<Score>();
            string allScores = PlayerPrefs.GetString("Scores");
            List<string> scoreStrings = new List<string>(allScores.Split(';'));
            for(int i = 0; i < scoreStrings.Count - 1; i += 3) {
                Score score = new Score(scoreStrings[i], scoreStrings[i+1], scoreStrings[i+2]);
                scores.Add(score);
            }
            return scores;
        }
        public void AddNewScore(Score newScore)
        {
            List<Score> scores = GetAllScores();
            scores.Add(newScore);
            scores = scores.OrderByDescending(score => score.scoreAsInt).ToList<Score>();
            scores.RemoveAt(scores.Count -1);
            String scoresString = String.Empty;
            foreach (Score score in scores)
            {
                scoresString += CreateScore(score.player, score.date, score.score);               
            }
            PlayerPrefs.SetString("Scores", scoresString);
        }
    }
}
