using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PauseButton pauseButton;
    private void Start()
    {
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
    }
}
