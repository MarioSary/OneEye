using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class TargetSensor : MonoBehaviour
{
    public GameObject detectedtarget;
    public GameObject blood;

    [SerializeField] private Trolley tram;

    public Trolley Tram
    {
        get => tram;
        set => tram = value;
    }


    void Start()
    {
        detectedtarget = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrainTarget")
        {
            detectedtarget = other.gameObject;
            //Debug.Log("target point detected!");
        }

        if (other.tag == "People")
        {
            Destroy(other.gameObject);

            Instantiate(blood, transform.position, Quaternion.identity);
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TrainTarget")
        {
            //Debug.Log("target point undetected!");
            detectedtarget = null;
        }
    }
}
