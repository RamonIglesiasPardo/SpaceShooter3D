using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Formations : MonoBehaviour
{
    public string[] formations;
    private GameObject[] enemies;
    public Transform spawnPoint;

    PlayerShipController playerShipController;
    void Start()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies");
        playerShipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
    }

    public void SpawnFormation()
    {
        string selectedFormation = formations[Random.Range(0, formations.Length)];
        Debug.Log(selectedFormation);
        GameObject selectedEnemy = enemies[Random.Range(0, enemies.Length)];

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
            case "Triangulo":
                Triangulo(selectedEnemy);
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
        Instantiate(enemy, new Vector3(spawnPoint.position.x - 2.0f, spawnPoint.position.y, spawnPoint.position.z), spawnPoint.rotation);
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
    void Triangulo(GameObject enemy)
    {
        float xDisponible = Math.Abs(playerShipController.xMin) + Math.Abs(playerShipController.xMax);
        
        //float xColiderNave = enemy.GetComponent<Collider>().bounds.size.x;
        float xColiderNave = 1f;
        float yColiderNave = 0.7f;
        float margenEntreNaves = 0.2f;
        float xNecesarioNave = xColiderNave + margenEntreNaves;
        float yNecesarioNave = yColiderNave + margenEntreNaves;
        var count = 0;
        float factorDeCorreccion;
        while (xColiderNave < xDisponible)
        {
            factorDeCorreccion = (count == 0) ? -1.0f : 0;
            Instantiate(enemy, new Vector3(playerShipController.xMin + xDisponible + margenEntreNaves, playerShipController.yMin, spawnPoint.position.z), spawnPoint.rotation);            
            float yDisponible = Math.Abs(playerShipController.yMin) + Math.Abs(playerShipController.yMax);
            while (yColiderNave < yDisponible)
            {
                Instantiate(enemy, new Vector3(playerShipController.xMin + xDisponible + margenEntreNaves, playerShipController.yMin + yDisponible + margenEntreNaves + factorDeCorreccion, spawnPoint.position.z), spawnPoint.rotation);
                yDisponible -= yNecesarioNave;
            }
            xDisponible -= xNecesarioNave;
        }
    }
}

// xDisponible = |xInicial| + |xFinal|  
// xNave = xColider + margenEntreNaves
// while (anchoNave * 2 < xDisponible)
// {
//   
// }


