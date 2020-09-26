using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehavior : MonoBehaviour
{
    public float speed;
    public string inFormation;
    public int scoreValue;
    Rigidbody rb;
    bool descending = false;
    public float speedFormation;
    Vector3 inFormVel;
    public int liveCount;
    public GameObject hitExplosion;
    public GameObject destroyExplosion;
    private GameController gameController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //Debug.Log(inFormation);
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector3.back * speed;

        switch (inFormation)
        {
            case "HLine":
                UpANdDown();
                break;          
            default:
                break;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            Debug.Log("<color=green>BaseEnemyBehavior: </color>Collision detected with player!!");
            Instantiate(destroyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void UpANdDown()
    {

        if (transform.position.y < -1)
        {
            descending = false;
        }
        else if (transform.position.y > 2)
        {
            descending = true;
        }
        if (descending)
        {
            inFormVel = new Vector3(0.0f, -1 * speedFormation, 0.0f);
        }
        else
        {
            inFormVel = new Vector3(0.0f, 1 * speedFormation, 0.0f);
        }
        rb.velocity += inFormVel;
    }

    public void HitCount()
    {
        Debug.Log("<color=green> BaseEnemyBehavior: </color>" + ": Tenia = " + liveCount + " | le quedan = " + --liveCount);
        
        liveCount -= 1;
        if (liveCount <= 0)
        {
            Instantiate(destroyExplosion, transform.position, transform.rotation);
            Debug.Log("<color=green> BaseEnemyBehavior: </color>" + ": Destruido!");
            Destroy(this.gameObject);
            gameController.IncreasePoints(scoreValue);
        } else
        {
            Instantiate(hitExplosion, transform.position, transform.rotation);                
        }
    }
}
