using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    protected GameObject[] enemies;
    protected GameObject[] asteroids;
    public GameObject enemy;
    public float xPos;
    public float yPos;
    public GameObject spawnPoint;

    void Start()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies");
        asteroids = Resources.LoadAll<GameObject>("Asteroids/Prefabs");
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(FormacionEnDelta(2, Random.Range(0, enemies.Length), 5));
        

    }

    IEnumerator SpawnAsteroids()
    {
        while (Application.isPlaying)
        {

            CreateRandomSpawnPoint();

            int randomAsteroidId = Random.Range(0, asteroids.Length);
            GameObject asteroidInstance = (GameObject)Instantiate(asteroids[randomAsteroidId], new Vector3(xPos, yPos, 60), Quaternion.identity);
            Debug.Log("<color=green>SpawnArea:</color> enemyInstance.GetComponent<Collider>().bounds.size = " + asteroidInstance.GetComponent<Collider>().bounds.size);

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    private void CreateRandomSpawnPoint()
    {
        xPos = Random.Range(-3.5f, 3.5f);
        yPos = Random.Range(-1, 2);
    }

    IEnumerator FormacionEnDelta(int numNaves, int randomEnemyId, int distanceOffset)
    {
        while (Application.isPlaying)
        {
            GameObject enemyInstance = (GameObject)Instantiate(enemies[randomEnemyId], new Vector3(xPos, yPos, 60), Quaternion.identity);
            for (int i = 0; i < numNaves; i++)
            {
                if (i == 0)
                {
                    Instantiate(enemyInstance, spawnPoint.transform.position, spawnPoint.transform.rotation);                   
                }
                else
                {
                    Vector3 positveOffsetInstance = new Vector3(spawnPoint.transform.position.x + (distanceOffset * i), spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                    Vector3 negativeOffsetInstance = new Vector3(spawnPoint.transform.position.x - (distanceOffset * i), spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                    Instantiate(enemyInstance, positveOffsetInstance, spawnPoint.transform.rotation);
                    Instantiate(enemyInstance, negativeOffsetInstance, spawnPoint.transform.rotation);                    
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}


//IEnumerator EnemySpawn()
//{
//    while (Application.isPlaying)
//    {

//        CreateRandomSpawnPoint();

//        int randomEnemyId = Random.Range(0, enemies.Length);
//        GameObject enemyInstance = (GameObject)Instantiate(enemies[randomEnemyId], new Vector3(xPos, yPos, 60), Quaternion.identity);
//        Debug.Log("<color=green>SpawnArea:</color> enemyInstance.GetComponent<Collider>().bounds.size = " + enemyInstance.GetComponent<Collider>().bounds.size);

//        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
//    }
//}

//private void CreateRandomSpawnPoint()
//{
//    xPos = Random.Range(-3.5f, 3.5f);
//    yPos = Random.Range(-1, 2);
//}