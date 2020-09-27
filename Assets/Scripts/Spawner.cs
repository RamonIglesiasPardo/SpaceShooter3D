using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public int level;
    public float waveStart;
    public float spawnRate;
    public bool isSpawning;
    public bool is3DTextSpawning;
    private float timeToStartNextWave;
    private bool shouldStop;

    private GameObject gameObject3DTextInstance;
    public GameObject gameObjectText3D;
    private FormationConstructor formations;
    private GameObject[] enemies;
    private GameObject[] asteroids;
    private PlayerShipController playerShipController;

    private void Start()
    {
        playerShipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShipController>();
        enemies = Resources.LoadAll<GameObject>("Enemies");
        asteroids = Resources.LoadAll<GameObject>("Asteroids/Prefabs");
        timeToStartNextWave = Time.time + waveStart;
        formations = GetComponent<FormationConstructor>();
    }

    private void Update()
    {
        if (Time.time > timeToStartNextWave && Application.isPlaying && isSpawning)
        {            
            GameObject element = (IsHardEnemy()) ? enemies[Random.Range(0, enemies.Length)] : asteroids[Random.Range(0, asteroids.Length)];
            //element = enemies[0];
            List <Vector3> spawndablesPositionForElement = GetSpawndablesPositionForElement(element, 0.3f);
            SpawnWave(element , spawndablesPositionForElement);
            timeToStartNextWave = Time.time + spawnRate;
        }
        if (shouldStop && is3DTextSpawning)
        {
            if(gameObject3DTextInstance.transform.position.z <= 3)
            {
                gameObject3DTextInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                is3DTextSpawning = false;
            }
        }
    }

    private List<Vector3> GetSpawndablesPositionForElement(GameObject element, float minMarginBetweenElements)
    {
        float playableAreaWidth = (new Vector2(playerShipController.xMax, 0.0f) - new Vector2(playerShipController.xMin, 0.0f)).magnitude;
        float playableAreaHeight = (new Vector2(0.0f, playerShipController.yMax) - new Vector2(0.0f, playerShipController.yMin)).magnitude;
        float elementWidth = element.GetComponent<Renderer>().bounds.size.x;
        float elementHeight = element.GetComponent<Renderer>().bounds.size.y;
        //float minMarginBetweenElements = element.GetComponent<Renderer>().bounds.size.x/5;
        float remaindereWidth = playableAreaWidth % (elementWidth + minMarginBetweenElements);
        float remainderHeight = playableAreaHeight % (elementHeight + minMarginBetweenElements);
        float maxShipsX = (float) Math.Floor(playableAreaWidth / (elementWidth + minMarginBetweenElements));
        float maxShipsY = (float) Math.Floor(playableAreaHeight / (elementHeight + minMarginBetweenElements));
        float marginBetweenShipsX = remaindereWidth / maxShipsX;
        float marginBetweenShipsY = remainderHeight / maxShipsY;        
        List <Vector3> spawndablesPositionForElement = new List<Vector3>();
        for (int i = 0; i <= maxShipsX; i++)
        {
            float x = playerShipController.xMin + ((elementWidth + marginBetweenShipsX + minMarginBetweenElements) * i);
            spawndablesPositionForElement.Add(new Vector3(x, playerShipController.yMin, this.transform.position.z));            
            for (int j = 1; j <= maxShipsY; j++)
            {
                float y = playerShipController.yMin + ((elementHeight + marginBetweenShipsY + minMarginBetweenElements) * j);
                spawndablesPositionForElement.Add(new Vector3(x, y, this.transform.position.z));
            }
        }
        return spawndablesPositionForElement;
    }    

    private bool IsHardEnemy()
    {
        int probabilitySpawnHardEnemy = System.Math.Min(level, 8);
        int randomSelection = Random.Range(0, 10);
        return randomSelection <= probabilitySpawnHardEnemy;
    }

    public void SpawnWave(GameObject element, List<Vector3> spawndablesPositionForElement)
    {
        GameObject ele = formations.Formation(element, spawndablesPositionForElement);
        foreach (Vector3 position in formations.positionsToSpawn)
        {
            ele.GetComponent<BasicBehavior>().inFormation = GetComponent<FormationConstructor>().formationInUse;

            GameObject go = Instantiate(ele, position, this.transform.rotation);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.velocity = Vector3.back * 150;
        }
        formations.positionsToSpawn.Clear();
    }

    public void Spawn3DText(string text, bool shouldStop, float distanceToStop)
    {
        this.shouldStop = shouldStop;
        gameObject3DTextInstance = Instantiate(gameObjectText3D, new Vector3(0,0, this.transform.position.z), Quaternion.identity);
        SimpleHelvetica simpleHelvetica = gameObject3DTextInstance.GetComponent<SimpleHelvetica>();
        simpleHelvetica.SetText(text);
        simpleHelvetica.Reset();        
        Rigidbody rb = gameObject3DTextInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.back * 20;
        is3DTextSpawning = true;
    }
}
