using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public bool pauseIsPressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        pauseIsPressed = !pauseIsPressed;
    }
}
