using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PauseButton pauseButton;

    private void Update()
    {
        if (pauseButton.pauseIsPressed)
        {
            Time.timeScale = 0.0f;
            Debug.Log("Game paused, in theory");
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
