using System.Collections.Generic;
using UnityEngine;

public class FormationConstructor : MonoBehaviour
{
    public string[] formations;
    public List<Vector3> positionsToSpawn = new List<Vector3>();

    public void Formation(GameObject element, List<Vector3> spawneablePositions)
    {
        bool isAsteroid = element.CompareTag("Asteroid");
        string[] asteroidsFormations = {"Single", "Random" };
        string[] shipsFormations = { "Complete", "HLine", "VLine", "Cross", "LeftDownTri", "RightDownTri", "LeftUpTri", "RightUpTri" };
        string selectedFormation = isAsteroid? asteroidsFormations[Random.Range(0, asteroidsFormations.Length)] : shipsFormations[Random.Range(0, shipsFormations.Length)];
        //selectedFormation = "Complete";
        switch (selectedFormation)
        {
            case "Single":
                positionsToSpawn.Add(spawneablePositions[Random.Range(0, spawneablePositions.Count)]);
                break;
            case "Random":
                int numOfElements = Random.Range(4, spawneablePositions.Count);
                for (int i = 0; i < numOfElements; i++)
                {
                    Vector3 randomFormationElement = spawneablePositions[Random.Range(0, spawneablePositions.Count)];
                    if (!positionsToSpawn.Contains(randomFormationElement)) positionsToSpawn.Add(randomFormationElement);
                }
                break;
            case "Complete":
                foreach (Vector3 completeFormationElement in spawneablePositions)
                {
                    positionsToSpawn.Add(completeFormationElement);
                }
                break;
            case "HLine":
                Vector3 hLineFormationElement = spawneablePositions[Random.Range(0, spawneablePositions.Count)];
                foreach (Vector3 position in spawneablePositions)
                {
                    if (hLineFormationElement.y == position.y) positionsToSpawn.Add(position);
                }
                break;
            case "VLine":
                Vector3 vLineFormationElement = spawneablePositions[Random.Range(0, spawneablePositions.Count)];
                foreach (Vector3 position in spawneablePositions)
                {
                    if (vLineFormationElement.x == position.x) positionsToSpawn.Add(position);
                }
                break;
            case "Cross":
                Vector3 crossFormationElement = spawneablePositions[Random.Range(0, spawneablePositions.Count)];
                foreach (Vector3 position in spawneablePositions)
                {
                    if (crossFormationElement.y == position.y || crossFormationElement.x == position.x) positionsToSpawn.Add(position);
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
                //TODO Redefine LeftDownTri
                //int colZeroCount = 0;
                //for (int i = 0; i < spawneablePositions.Count; i++)
                //{
                //    if (i == 0 || spawneablePositions[i].x != spawneablePositions[i-1].x)
                //    {
                //        positionsToSpawn.Add(spawneablePositions[i]);                        
                //    }
                //    if (spawneablePositions[i].x == spawneablePositions[0].x)
                //    {
                //        colZeroCount++; 
                //    }
                //}                
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
                break;        }
        spawneablePositions.Clear();
    }
}
