using System.Collections;
using UnityEngine;

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
    protected bool inmune;
    public int timeInmune;
    public AudioClip audioPower;
    public AudioClip inmunityOn;
    public AudioClip inmunityOff;
    public AudioClip audioShoot;
    public AudioClip audioExplosion;
    GameController gameController;
    AudioSource audioFont;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lives = 3;
        timeInmune = 20;
        gameController.ShowHUDLives(lives);
        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
        rb = GetComponent<Rigidbody>();
        laserbeamControl = GameObject.FindGameObjectWithTag("LaserSystem").GetComponent<LaserBeamControl>();
        audioFont = GetComponent<AudioSource>();
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
            audioFont.clip = audioShoot;
            audioFont.Play();
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
            if (other.gameObject.CompareTag("PowerUpLive"))
            {
                Debug.Log("<color=red>BaseEnemyBehavior: </color>PowerUpLive with player!!");
                if (lives < 5)
                {
                    lives = lives + 1;
                    audioFont.clip = audioPower;
                    audioFont.Play();
                    gameController.ShowHUDLives(lives);
                    Destroy(other.gameObject);
                }
            }
            else if (other.gameObject.CompareTag("PowerUpVelocity"))
            {
                StartCoroutine (Inmune ());
                Destroy(other.gameObject);
            }
            else
            {
                if (!inmune)
                {
                    lives--;
                    if (lives <= 0 && isTriggered)
                    {
                        lives = 0;
                        Instantiate(playerExplosion, transform.position, transform.rotation);                      
                        Destroy(gameObject);
                        gameController.GameOver();
                        isTriggered = false;
                    }
                }
            }
            if(isTriggered) gameController.ShowHUDLives(lives);
        }
        
        IEnumerator Inmune()
        {
            inmune = true;
            audioFont.clip = inmunityOn;
            audioFont.Play();
            yield return new WaitForSeconds(timeInmune);
            audioFont.clip = inmunityOff;
            audioFont.Play();
            inmune = false;
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