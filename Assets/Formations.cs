using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formations : MonoBehaviour
{
    public string[] formationsArray;
    public GameObject[] enemiesArray;
    public Transform spawnPoint;

    public void SpawnFormation()
    {
        string selectedFormation = formationsArray[Random.Range(0, formationsArray.Length)];
        Debug.Log(selectedFormation);
        GameObject selectedEnemy = enemiesArray[Random.Range(0, enemiesArray.Length)];

        switch (selectedFormation)
        {
            case "Single":
                SingleFormation(selectedEnemy);
                break;
            case "Double":
                DoubleFormation(selectedEnemy);
                break;
            case "Random":
                RandomFormation(selectedEnemy, Random.Range(3, 6));
                break;
            case "DeltaXZ":
                DeltaXZFormation(selectedEnemy, Random.Range(1, 4));
                break;
            default:
                break;
        }
    }

    void SingleFormation(GameObject enemy)
    {
        spawnPoint.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.0f, 2.0f), spawnPoint.position.z);
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    void DoubleFormation(GameObject enemy)
    {
        spawnPoint.position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-1.0f, 2.0f), spawnPoint.position.z);
        Instantiate(enemy, new Vector3(spawnPoint.position.x - 2.0f,spawnPoint.position.y,spawnPoint.position.z), spawnPoint.rotation);
        Instantiate(enemy, new Vector3(spawnPoint.position.x + 2.0f, spawnPoint.position.y, spawnPoint.position.z), spawnPoint.rotation);
    }

    void RandomFormation(GameObject enemy, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SingleFormation(enemy);
        }
    }

    void DeltaXZFormation(GameObject enemy, int enemyCount)
    {
        spawnPoint.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1.0f, 2.0f), spawnPoint.position.z);
        for (int i = 0; i < enemyCount; i++)
        {
            if (i == 0)
            {
                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(enemy, new Vector3(spawnPoint.position.x - 1.5f * i, spawnPoint.position.y, spawnPoint.position.z + 1.0f * i), spawnPoint.rotation);
                Instantiate(enemy, new Vector3(spawnPoint.position.x + 1.5f * i, spawnPoint.position.y, spawnPoint.position.z + 1.0f * i), spawnPoint.rotation);
            }
        }
    }
}
