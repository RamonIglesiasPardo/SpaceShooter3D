using Boo.Lang;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int level;
    public float waveStart;
    public float spawnRate;
    private float wave;

    private FormationConstructor formations;
    private GameObject[] enemies;
    private GameObject[] asteroids;
    private PlayerShipController playerShipController;
    public Transform spawnPoint;

    private void Start()
    {
        playerShipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
        enemies = Resources.LoadAll<GameObject>("Enemies");
        asteroids = Resources.LoadAll<GameObject>("Asteroids/Prefabs");
        wave = Time.time + waveStart;
        formations = GetComponent<FormationConstructor>();
    }

    private void Update()
    {
        if (Time.time > wave)
        {            
            GameObject element = (IsHardEnemy()) ? enemies[Random.Range(0, enemies.Length)] : asteroids[Random.Range(0, asteroids.Length)];
            spawnPoint.position = new Vector3(playerShipController.xMin, playerShipController.yMin, spawnPoint.position.z);
            SpawnWave(element);
            wave = Time.time + spawnRate;
        }
    }

    private bool IsHardEnemy()
    {
        int probabilityEnemy = System.Math.Min(level * 1, 9);
        int randomSelection = Random.Range(0, 10);
        return randomSelection <= probabilityEnemy;
    }

    void SpawnWave(GameObject element)
    {
        formations.Formation(element, spawnPoint);
        foreach (Vector3 position in formations.positionsToSpawn)
        {
            Instantiate(element, position, spawnPoint.rotation);
        }
        formations.positionsToSpawn.Clear();
    }
}
