using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBehavior : MonoBehaviour
{
    //protected Rigidbody rb;
    //public float speed;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 15);
        if (transform.position.z <= -10)
        {
            Destroy(gameObject);
        }
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
