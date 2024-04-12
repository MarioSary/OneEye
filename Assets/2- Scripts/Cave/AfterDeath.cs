using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDeath : MonoBehaviour
{
    public GameObject goInside;
    public GameObject orb;


    private void Update()
    {
        if (!Enemy.alive && orb == null)
        {
            for (int i = 0; i < RefGameObjectsEvent.instance.playerCameraTriggers.Count; i++)
            {
                RefGameObjectsEvent.instance.playerCameraTriggers[i].SetActive(true);
            }
            RefGameObjectsEvent.instance.NPCs.SetActive(true);
            goInside.SetActive(true);
            RefGameObjectsEvent.instance.battleBounds.SetActive(false);
        }
    }
    
}
