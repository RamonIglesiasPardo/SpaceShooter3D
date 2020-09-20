using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationConstructor : MonoBehaviour
{
    private PlayerShipController playerShipController;

    public string[] formations;
    public List<Vector3> positionsToSpawn = new List<Vector3>();
    public List<Vector3> spawneablePositions = new List<Vector3>();

    void Start()
    {       
        playerShipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
    }

    public void Formation(GameObject element, Transform spawnPoint)
    {
        float anchoArea = (new Vector2(playerShipController.xMax, 0.0f) - new Vector2(playerShipController.xMin, 0.0f)).magnitude;
        float altoArea = (new Vector2(0.0f, playerShipController.yMax) - new Vector2(0.0f, playerShipController.yMin)).magnitude;
        float anchoNave = 1f;
        float altoNave = 1f;
        float restoAncho = anchoArea % anchoNave;
        float restoAlto = altoArea % altoNave;
        float navesMaxX = (anchoArea - restoAncho) / anchoNave;
        float navesMaxY = (altoArea - restoAlto) / altoNave;
        bool isAsteroid = element.CompareTag("Asteroid");
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
        int navesEnX = 0;
        foreach (Vector3 position in spawneablePositions)
        {
            if (spawneablePositions[0].y == position.y)
            {
                navesEnX++;
            }
        }
        float margenParaLlenarX = restoAncho / navesEnX;
        for (int i = 0; i < spawneablePositions.Count; i++)
        {
            Vector3 vector = spawneablePositions[i];
            vector.x = spawneablePositions[i].x + margenParaLlenarX;
            spawneablePositions[i] = vector;
        }
        
        //----------------------------------------------//
        //Escogemos Formación   //y = (-3/7)x + (1/2)
        string selectedFormation = formations[Random.Range(0, formations.Length)];
        Debug.Log(selectedFormation);
        switch (selectedFormation)
        {
            case "Single":
                positionsToSpawn.Add(startPosition);
                break;

            case "Random":
                int numOfShips = Random.Range(1, 7);
                for (int i = 0; i < numOfShips; i++)
                {
                    Vector3 choosenPosition = spawneablePositions[Random.Range(0, spawneablePositions.Count)];
                    if (!positionsToSpawn.Contains(choosenPosition))
                    {
                        positionsToSpawn.Add(choosenPosition);
                    }
                }
                break;

            case "Complete":
                foreach (Vector3 position in spawneablePositions)
                {
                    positionsToSpawn.Add(position);
                }
                break;

            case "HLine":
                foreach(Vector3 position in spawneablePositions)
                {
                    if (position.y == startPosition.y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "VLine":
                foreach(Vector3 position in spawneablePositions)
                {
                    if (position.x == startPosition.x)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "Cross":
                foreach(Vector3 position in spawneablePositions)
                {
                    if (position.x == startPosition.x || position.y == startPosition.y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "LeftDownTri":
                foreach (Vector3 position in spawneablePositions)
                {
                    float y = (-3.0f / 7.0f) * position.x + (1.0f / 2.0f);
                    if (position.y <= y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "RightDownTri":
                foreach (Vector3 position in spawneablePositions)
                {
                    float y = (3.0f / 7.0f) * position.x + (1.0f / 2.0f);
                    if (position.y <= y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "LeftUpTri":
                foreach (Vector3 position in spawneablePositions)
                {
                    float y = (3.0f / 7.0f) * position.x + (1.0f / 2.0f);
                    if (position.y >= y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;

            case "RightUpTri":
                foreach (Vector3 position in spawneablePositions)
                {
                    float y = (-3.0f / 7.0f) * position.x + (1.0f / 2.0f);
                    if (position.y >= y)
                    {
                        positionsToSpawn.Add(position);
                    }
                }
                break;
        }

        spawneablePositions.Clear();
    }

    void LlenarX(Vector3 posicionEnY, float maxNavesX, float anchoNave)
    {
        Vector3 newXPosition = posicionEnY;
        for (int i = 1; i <= maxNavesX; i++)
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
        if (position.x > playerShipController.xMin && position.x < playerShipController.xMax
            && position.y > playerShipController.yMin && position.y < playerShipController.yMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
