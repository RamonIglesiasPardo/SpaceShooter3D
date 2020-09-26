using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruction : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }
}
