using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PauseButton pauseButton;
    public Image[] lives;
    int livesCount;
    private TextMeshProUGUI scoreHUD;
    private int score;
    int tmp = 0;
    PlayerShipController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
        scoreHUD = GameObject.FindGameObjectWithTag("scoreText").GetComponent<TMPro.TextMeshProUGUI>();
        score = 0;
        scoreHUD.SetText(score.ToString());
        livesCount = 3;
        //Canvas CanvasObject = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        GameObject[] joystickElements = GameObject.FindGameObjectsWithTag("Joystick");
        if (PlayerPrefs.GetInt("ShowHUD") == 0)
        {            
            foreach (GameObject element in joystickElements)
            {
                element.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
            foreach (GameObject element in GameObject.FindGameObjectsWithTag("ControlButton"))
            {
                element.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        if (PlayerPrefs.GetInt("UseAccelerometer") == 1)
        {           
            foreach (GameObject element in joystickElements)
            {
                element.SetActive(false);
            }
        }        
    }
    private void Update()
    {
        Time.timeScale = pauseButton.pauseIsPressed ? 0.0f : 1.0f;
        //IncreaseScore();
        
    }

    public void IncreasePoints(int points)
    {
        tmp = score;
        score += points;
        StartCoroutine(ScoreUpdater());
    }
    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (tmp < score)
            {
                tmp++;
                scoreHUD.SetText(tmp.ToString());
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ShowHUDLives(int livesAmount)
    {
        livesCount = livesAmount;
        for (int i = 0; i < livesCount; i++)
        {
            lives[i].enabled = true;
        }
        for (int i = livesCount; i < lives.Length; i++)
        {
            lives[i].enabled = false;
        }
    }

}
