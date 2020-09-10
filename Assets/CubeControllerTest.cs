using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;



public class CubeControllerTest : MonoBehaviour
{    
    Rigidbody rb;
    public float speed;
    public float xMin, xMax, yMin, yMax;
    public GameObject particle;

    private void Start()
    {
        speed = 10;
        rb = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.velocity = movement * speed;
        rb.position = new Vector3
        (
        Mathf.Clamp(transform.position.x, xMin, xMax),
        Mathf.Clamp(transform.position.y, yMin, yMax),
        0.0f
        );

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    // Create a particle if hit
                    Instantiate(particle, transform.position, transform.rotation);
                }
            }
        }

    }  

}
