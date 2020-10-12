using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private BackGroundController backGroundController;
    public int scoreAmountToIncreaseLvl;
    public PauseButton pauseButton;
    private TextMeshProUGUI scoreHUD;
    public Image[] lives;
    private int livesCount;
    private int score;
    private bool isBackToMenuAllowed;
    Spawner spawner;
    private void Start()
    {
        InvokeRepeating("increaseScoreEachSecond", 1f, 1f);
        backGroundController = GameObject.FindGameObjectWithTag("Background").GetComponent<BackGroundController>();
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

    private void increaseScoreEachSecond()
    {
        score++;
        StartCoroutine(ScoreUpdater());
    }

    private void Update()
    {
        Time.timeScale = pauseButton.pauseIsPressed ? 0.0f : 1.0f;
        if (Input.anyKey && isBackToMenuAllowed) SceneManager.LoadScene(0);
        IncreaseLevel();
    }

    private int tmplevel = 1;
    

    private void IncreaseLevel()
    {        
        if(score > scoreAmountToIncreaseLvl * tmplevel)
        {
            spawner.Spawn3DText("Lvl " + tmplevel++, false, 0);
            spawner.level++;
            backGroundController.panSpeed += 0.05f;
            if (spawner.spawnRateEnemies >= 1) spawner.spawnRateEnemies -= 0.2f;  
        }
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
        spawner.Spawn3DText("Game\nOver", true, 20f);
        new Score().AddNewScore(new Score(PlayerPrefs.GetString("PlayerName"), DateTime.Now, score));
        pauseButton.enabled = false;
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
