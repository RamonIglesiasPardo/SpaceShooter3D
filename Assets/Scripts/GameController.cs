using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PauseButton pauseButton;
    private TextMeshProUGUI scoreHUD;
    public Image[] lives;
    private int livesCount;
    private int score;
    private bool isBackToMenuAllowed;

    Spawner spawner;
    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        scoreHUD = GameObject.FindGameObjectWithTag("scoreText").GetComponent<TMPro.TextMeshProUGUI>();
        score = 0;
        isBackToMenuAllowed = false;
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
        if (Input.anyKey && isBackToMenuAllowed) SceneManager.LoadScene(0);
    }
    private int tmp = 0;
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

    public void GameOver()
    {
        spawner.isSpawning = false;
        AcelerateRemainingEnemies();

        
        //  paraeljuego
        //  mostrarpuntuancion
        new Score().AddNewScore(new Score(PlayerPrefs.GetString("PlayerName"), DateTime.Now, score));
        StartCoroutine(AnyKeyToBackMenu());
    }
    private void AcelerateRemainingEnemies()
    {
        float force = 5000f;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject asteroid in asteroids)
        {
            asteroid.GetComponent<Rigidbody>().AddForce(asteroid.transform.forward * force);
        }
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody>().AddForce(enemy.transform.forward * force, ForceMode.Acceleration);
        }
    }

    IEnumerator AnyKeyToBackMenu()
    {        
        yield return new WaitForSeconds(3);
        isBackToMenuAllowed = true;        
    }
}
