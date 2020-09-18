using Boo.Lang;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float waveRate;
    public float waveStart;
    private float wave;
    private FormationConstructor formations;
    private GameObject[] enemies;
    public Transform spawnPoint;

    private void Start()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies");
        wave = Time.time + waveStart;
        formations = GetComponent<FormationConstructor>();
    }

    private void Update()
    {
        if (Time.time > wave)
        {
            spawnPoint.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.0f, 2.0f), spawnPoint.position.z);
            SpawnWave();
            wave = Time.time + waveRate;
        }
    }

    void SpawnWave()
    {
        formations.Formation();
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        foreach (Vector3 position in formations.positionsToSpawn)
        {
            Instantiate(enemy, position, spawnPoint.rotation);
        }
        formations.positionsToSpawn.Clear();
    }
}
