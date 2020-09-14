using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float waveRate;
    public float waveStart;
    

    private Vector3 spawnPosition;
    private float wave;
    private Formations formations;

    private void Start()
    {
        wave = Time.time + waveStart;
        formations = GetComponent<Formations>();
    }

    private void Update()
    {
        if (Time.time > wave)
        {
            formations.SpawnFormation();
            wave = Time.time + waveRate;
        }
    }
}
