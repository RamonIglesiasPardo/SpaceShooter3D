using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        int x = 0;
        if (Int32.TryParse(score, out x)) {
                scoreAsInt = Int32.Parse(score);
                };
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
        for (int i = 0; i < scoreStrings.Count - 1; i += 3)
        {
            Score score = new Score(scoreStrings[i], scoreStrings[i + 1], scoreStrings[i + 2]);
            scores.Add(score);
        }
        return scores;
    }
    public void AddNewScore(Score newScore)
    {
        List<Score> scores = GetAllScores();
        scores.Add(newScore);
        scores = scores.OrderByDescending(score => score.scoreAsInt).ToList<Score>();
        scores.RemoveAt(scores.Count - 1);
        String scoresString = String.Empty;
        foreach (Score score in scores)
        {
            scoresString += CreateScore(score.player, score.date, score.score);
        }
        PlayerPrefs.SetString("Scores", scoresString);
    }
}
