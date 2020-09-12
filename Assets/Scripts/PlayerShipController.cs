using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{    
    protected Rigidbody rb;
    public float speed;
    public float tilt;
    public float xMin, xMax, yMin, yMax;
    protected Joystick joystick;
    protected JoyButton joyButton;
    protected bool shoot;
    private LaserBeamControl laserbeamControl;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
        rb = GetComponent<Rigidbody>();
        laserbeamControl = GameObject.FindGameObjectWithTag("LaserSystem").GetComponent<LaserBeamControl>();
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
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        if (!shoot && joyButton.Pressed)
        {
            shoot = true;
            laserbeamControl.SwitchLaserBeam();          
            Debug.Log("<color=green>ShipController: </color>We are shooting!!");

        }

        if (shoot && !joyButton.Pressed)
        {
            shoot = false;
        }

    }

}
