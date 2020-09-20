using UnityEngine;
using System.Collections;

public class TumbleBehavior : MonoBehaviour
{
    public float tumble;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
