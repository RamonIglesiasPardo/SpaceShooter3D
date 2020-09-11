using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    new Renderer renderer;
    public float panSpeed;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0.0f, Time.time * panSpeed);
        renderer.material.mainTextureOffset = offset;
    }
}
