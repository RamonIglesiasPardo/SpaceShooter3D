using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float waveRate;
    public float waveStart;
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
