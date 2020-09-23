using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public bool pauseIsPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pauseIsPressed = !pauseIsPressed;
        Debug.Log(pauseIsPressed);
    }
}
