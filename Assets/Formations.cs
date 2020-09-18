using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Formations : MonoBehaviour
{
    public string[] formations;
    private GameObject[] enemies;
    public Transform spawnPoint;

    PlayerShipController playerShipController;

    //Nueva Version
    public List<Vector3> positionsToSpawn = new List<Vector3>();
    public List<Vector3> spawneablePositions = new List<Vector3>();

    void Start()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies");
        playerShipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
        
    }

    public void SpawnFormation()
    {
        string selectedFormation = formations[Random.Range(0, formations.Length)];
        //Debug.Log(selectedFormation);
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
            case "Entire":
                EntireFormation();
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

    public void EntireFormation()
    {

        float anchoArea = (new Vector2(playerShipController.xMax, 0.0f) - new Vector2(playerShipController.xMin, 0.0f)).magnitude;
        float altoArea = (new Vector2(0.0f, playerShipController.yMax) - new Vector2(0.0f, playerShipController.yMin)).magnitude;
        float anchoNave = 1.2f;
        float altoNave = 0.8f;
        float restoAncho = anchoArea % anchoNave;
        float restoAlto = altoArea % altoNave;
        float navesMaxX = (anchoArea - restoAncho) / anchoNave;
        float navesMaxY = (altoArea - restoAlto) / altoNave;
               
        //Debug.Log(anchoArea + ", " + altoArea + "; " + restoAncho + ", " + restoAlto + ";" + navesMaxX + "," + navesMaxY);

        Vector3 startPosition = spawnPoint.position;
        spawneablePositions.Add(startPosition);
        Vector3 newPosition;
        
        newPosition = startPosition;
        LlenarX(newPosition, navesMaxX, anchoNave);
        //llenamos la Y
        for (int i = 1; i <= navesMaxY; i++)
        {
            newPosition.y += altoNave;
            if (InsideLimits(newPosition))
            {
                spawneablePositions.Add(newPosition);
            }
            LlenarX(newPosition, navesMaxX, anchoNave);
        }
        newPosition = startPosition;
        for (int i = 1; i <= navesMaxY; i++)
        {
            newPosition.y -= altoNave;
            if (InsideLimits(newPosition))
            {
                spawneablePositions.Add(newPosition);
            }
            LlenarX(newPosition, navesMaxX, anchoNave);
        }

        /*foreach(Vector3 position in spawneablePositions)
        {
            positionsToSpawn.Add(position);
        }*/

        /*foreach(Vector3 position in spawneablePositions)
        {
            if (position.y == startPosition.y)
            {
                positionsToSpawn.Add(position);
            }
        }*/

        //y = (-3/7)x + (1/2)
        /*foreach (Vector3 position in spawneablePositions)
         {
             float y = (-3.0f / 7.0f) * position.x + (1.0f / 2.0f);
             if (position.y <= y)
             {
                 positionsToSpawn.Add(position);
             }
         }*/
        /*foreach (Vector3 position in spawneablePositions)
        {
            float y = (-3.0f / 7.0f) * position.x + (1.0f / 2.0f);
            if (position.y >= y)
            {
                positionsToSpawn.Add(position);
            }
        }*/
    }

    void LlenarX(Vector3 posicionEnY, float maxNavesX, float anchoNave)
    {
        Vector3 newXPosition = posicionEnY;
        for(int i = 1; i <= maxNavesX; i++)
        {
            newXPosition.x += anchoNave;
            if (InsideLimits(newXPosition))
            {
                spawneablePositions.Add(newXPosition);
            }
        }
        newXPosition = posicionEnY;
        for (int i = 1; i <= maxNavesX; i++)
        {
            newXPosition.x -= anchoNave;
            if (InsideLimits(newXPosition))
            {
                spawneablePositions.Add(newXPosition);
            }
        }
    }
    
    bool InsideLimits(Vector3 position)
    {
        if(position.x > playerShipController.xMin && position.x < playerShipController.xMax 
            && position.y > playerShipController.yMin && position.y < playerShipController.yMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }










   /* void Triangulo(GameObject enemy)
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
    }*/
}

// xDisponible = |xInicial| + |xFinal|  
// xNave = xColider + margenEntreNaves
// while (anchoNave * 2 < xDisponible)
// {
//   
// }


