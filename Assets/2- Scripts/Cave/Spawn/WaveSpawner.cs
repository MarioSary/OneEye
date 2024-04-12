using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private Wave currentWave;

    [SerializeField] private Transform[] spawnPoints;
    private float timeBtwSpawns;
    private int i = 0;
    private bool stopSpawning = false;

    private void Awake()
    {
        currentWave = waves[i];
        timeBtwSpawns = currentWave.TimeBeforeThisWave;
    }

    private void Update()
    {
        if (stopSpawning)
        {
            return;
        }

        if (Time.time >= timeBtwSpawns)
        {
            SpawnWave();
            IncWave();
            
            timeBtwSpawns = Time.time + currentWave.TimeBeforeThisWave;
        }
    }

    void SpawnWave()
    {
        for (int j = 0; j < currentWave.NumberToSpawn; j++)
        {
            int num = Random.Range(0, currentWave.EnemiesInWave.Length);
            int num2 = Random.Range(0, spawnPoints.Length);

            //Instantiate(currentWave.EnemiesInWave[num], spawnPoints[num2].position, spawnPoints[num2].rotation);
            Instantiate(currentWave.EnemiesInWave[num], spawnPoints[num2].position, Quaternion.identity);
        }
    }

    void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
            
        }
        else
        {
            i = 0;
        }
        
    }
}
