using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class CubeControllerTest : MonoBehaviour
{    
    protected Rigidbody rb;
    public float speed;
    public float xMin, xMax, yMin, yMax;
    protected Joystick joystick;
    protected JoyButton joyButton;
    protected bool shoot;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
        speed = 10f;
        rb = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        float moveHorizontal = joystick.Horizontal + Input.GetAxis("Horizontal");
        float moveVertical = joystick.Vertical + Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.velocity = movement * speed;
        rb.position = new Vector3
        (
        Mathf.Clamp(transform.position.x, xMin, xMax),
        Mathf.Clamp(transform.position.y, yMin, yMax),
        0.0f
        );

        if (!shoot && joyButton.Pressed)
        {
            shoot = true;
            Debug.Log("<color=green>ShipController: </color>We are shooting!!");

        }

        if (shoot && !joyButton.Pressed)
        {
            shoot = false;
        }

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        //rb.velocity = movement * speed;
        //rb.position = new Vector3
        //(
        //Mathf.Clamp(transform.position.x, xMin, xMax),
        //Mathf.Clamp(transform.position.y, yMin, yMax),
        //0.0f
        //);



        //Vector3 dir = Vector3.zero;
        //// we assume that the device is held parallel to the ground
        //// and the Home button is in the right hand

        //// remap the device acceleration axis to game coordinates:
        //// 1) XY plane of the device is mapped onto XZ plane
        //// 2) rotated 90 degrees around Y axis

        //dir.x = -Input.acceleration.y;
        //dir.z = Input.acceleration.x;

        //// clamp acceleration vector to the unit sphere
        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();

        //// Make it move 10 meters per second instead of 10 meters per frame...
        //dir *= Time.deltaTime;

        //// Move object
        //transform.Translate(dir * speed);

    }

}
