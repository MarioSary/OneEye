using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intersection : MonoBehaviour
{
    public GameObject[] wpA;
    public GameObject[] wpB;
    
    public Slider slider;

    
    void Start()
    {
        EnableIntersection(0);
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void EnableIntersection(int side)
    {

        if (side == 0)
        {
            //activate the straight rail
            foreach (GameObject point in wpA)
            {
                point.SetActive(true);
            }
            foreach (GameObject point in wpB)
            {
                point.SetActive(false);
            }
        }
        else
        {
            //activate the other rail
            foreach (GameObject point in wpA)
            {
                point.SetActive(false);
            }
            foreach (GameObject point in wpB)
            {
                point.SetActive(true);
            }
        }
    }

    public void ValueChangeCheck()
    {
        if (slider.value == 0f)
        {
            EnableIntersection(0);
        }
        else
        {
            EnableIntersection(1);
        }
        
    }
}
