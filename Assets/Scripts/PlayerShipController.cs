using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShipController : MonoBehaviour
{
    public GameObject playerExplosion;
    protected Rigidbody rb;
    public float speed;
    public float tilt;
    public float xMin, xMax, yMin, yMax;
    protected Joystick joystick;
    protected JoyButton joyButton;
    protected bool shoot;
    private LaserBeamControl laserbeamControl;
    public float tumble;
    public int lives;
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lives = 3;
        gameController.ShowHUDLives(lives);
        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
        rb = GetComponent<Rigidbody>();
        laserbeamControl = GameObject.FindGameObjectWithTag("LaserSystem").GetComponent<LaserBeamControl>();
    }

    private void FixedUpdate()
    {
        if (transform.position.z < 0.0f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
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
    private bool isTriggered = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Limit"))
        {
            lives--;            
            if (lives <= 0 && isTriggered)
            {
                lives = 0;
                Instantiate(playerExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
                gameController.GameOver();                
                SceneManager.LoadScene(0);
                isTriggered = false;
            }
            if(isTriggered) gameController.ShowHUDLives(lives);
        }
    }
    //Pendiente de buscar que tumblee cuando impacta la nave del jugador con un enemigo/asteroide

    //void OnTriggerEnter(Collider other)
    //{
    //    if (!other.gameObject.CompareTag("Limit"))
    //    {
    //        Vector3 currentPos = transform.position;
    //        Vector3 currentRotation = transform.rotation.eulerAngles;
    //        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    //        StartCoroutine(RestorePosition(currentPos, currentRotation));
    //    }


    //    //Destroy(gameObject);

    //}
    //private IEnumerator RestorePosition(Vector3 currentPos, Vector3 currentRotation)
    //{
    //    yield return new WaitForSeconds(2f);
    //    Lerp(currentPos, currentRotation);
    //}

    //private IEnumerator Lerp(Vector3 currentPos, Vector3 currentRotation)
    //{
    //    float timeElapsed = 0;
    //    float lerpDuration = 2;
    //    Vector3 valueToLerp = new Vector3();

    //    while (timeElapsed < 2)
    //    {
    //        valueToLerp = Vector3.Lerp(transform.position, currentPos, timeElapsed / lerpDuration);
    //        timeElapsed += Time.deltaTime;

    //        yield return null;
    //    }
    //    transform.Translate(valueToLerp, Space.Self);
    //}

}