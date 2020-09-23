using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehavior : MonoBehaviour
{
    public float speed;
    public string inFormation;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(inFormation);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.back * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            Debug.Log("<color=green>BaseEnemyBehavior: </color>Collision detected with player!!");
            Destroy(gameObject);
        }
    }
}
