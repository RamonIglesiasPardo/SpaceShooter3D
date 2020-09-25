using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDestruction : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}

//float y = (-3.0f / 7.0f) * position.x + (1.0f / 2.0f);
//if (position.y <= y)
//{
//    positionsToSpawn.Add(position);
//}