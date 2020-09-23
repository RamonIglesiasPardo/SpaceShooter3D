using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PauseButton pauseButton;

    private void Update()
    {
        Time.timeScale = pauseButton.pauseIsPressed ? 0.0f : 1.0f;
    }
}

