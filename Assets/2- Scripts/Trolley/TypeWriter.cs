using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class TypeWriter : MonoBehaviour
{
    public float delay = 0.1f;

    public string fullText;
    public string[] sentences;

    private string currentText = "";
    
    void Start()
    {
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
    
}
