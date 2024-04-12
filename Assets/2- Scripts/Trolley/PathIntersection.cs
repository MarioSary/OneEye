using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class PathIntersection : MonoBehaviour
{
    public GameObject [] trolleyPath5;

    public GameObject [] trolleyPath1;

    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        EnableIntersection(0f);
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableIntersection(float part)
    {
        Debug.Log("Current part activated: " + part);
        if (part == 0)
        {
            //activate the left railRoad
            foreach (GameObject point in trolleyPath5)
            {
                point.SetActive(true);
            }
            foreach (GameObject point in trolleyPath1)
            {
                point.SetActive(false);
            }
        }
        else
        {
            //activate the right railRoad
            foreach (GameObject point in trolleyPath5)
            {
                point.SetActive(false);
            }
            foreach (GameObject point in trolleyPath1)
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
