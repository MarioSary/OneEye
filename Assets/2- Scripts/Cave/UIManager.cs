using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    private int gemCount;
    [SerializeField] private TMP_Text gemText;
    
    [SerializeField] private Sprite orbFill;
    [SerializeField] private GameObject CaveOrbPlace;

    public GameObject weaponText;
    public GameObject attackText;
    public GameObject dashText;
    public GameObject tutorialLimit;

    [SerializeField] private Transform lifeParent;
    [SerializeField] private GameObject lifePrefab;

    private Stack<GameObject> lives = new Stack<GameObject>();

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    
    public void AddGem()
    {
        gemCount++;
        gemText.text = gemCount.ToString();
    }

    public void AddOrb()
    {
        CaveOrbPlace.GetComponent<Image>().sprite = orbFill;
        CaveOrbPlace.GetComponent<Image>().color = Color.white;
    }

    public void AddLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            lives.Push(Instantiate(lifePrefab, lifeParent));
        }
    }

    public void RemoveLife()
    {
        GameObject.Destroy(lives.Pop());
    }
}
