using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] spawnList;
    public List<GameObject> usableSpawnPoints;

    public Camera playerCamera;

    private Random random = new Random();

    // Start is called before the first frame update
    void Awake()
    {
        spawnList = GameObject.FindGameObjectsWithTag("EnemySpawn");
        if(playerCamera == null)
            playerCamera = Camera.main;
        Assert.IsNotNull(playerCamera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindSpawnPoints();
    }
    
    void FindSpawnPoints()
    {
        usableSpawnPoints.Clear();
        foreach (var spawnPoint in spawnList)
        {
            var viewPortPoint = playerCamera.WorldToViewportPoint(spawnPoint.transform.position);
            // Is in the viewport
            if (viewPortPoint.x > 0 && viewPortPoint.x < 1 
                                    && viewPortPoint.y > 0 && viewPortPoint.y < 1)
            continue;
            
            usableSpawnPoints.Add(spawnPoint);
        }

    }

    public void SpawnEnemy(GameObject gameObject)
    {
        var spawnPointIndex = random.Next(0, usableSpawnPoints.Count);
        var spawnPoint = usableSpawnPoints[spawnPointIndex];
        Instantiate(gameObject, spawnPoint.transform.position, Quaternion.identity);
    }
}
