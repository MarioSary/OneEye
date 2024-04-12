using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyPlaceEnviroSprites : MonoBehaviour
{
    public GameObject[] allSprites, foliage;

    public int copiesToMake, foliageCopies;

    public float minDistance, maxDistance;

    private Vector3 spawnPoint;
    
    void Start()
    {
        foreach(GameObject spr in allSprites)
        {
            SelectSpawnPoint(minDistance);

            spr.transform.position = spawnPoint;

            for(int i = 0; i < copiesToMake; i++)
            {
                SelectSpawnPoint(minDistance);

                Instantiate(spr, spawnPoint, spr.transform.rotation).transform.parent = transform;
            }
        }

        foreach(GameObject fol in foliage)
        {
            SelectSpawnPoint(0f);

            fol.transform.position = spawnPoint;

            for (int i = 0; i < foliageCopies; i++)
            {
                SelectSpawnPoint(0f);

                Instantiate(fol, spawnPoint, fol.transform.rotation).transform.parent = transform;
            }
        }
    }

   
    void Update()
    {
        
    }

    public void SelectSpawnPoint(float minDist)
    {
        spawnPoint.x = Random.Range(-10f, 2f);
        spawnPoint.z = Random.Range(-0.5f, 0.5f);
        spawnPoint.y = 0.06f;

        spawnPoint = Vector3.ClampMagnitude(spawnPoint, 1f) * maxDistance;

        if(spawnPoint.magnitude < minDist)
        {
            SelectSpawnPoint(minDist);
        }
    }
}
