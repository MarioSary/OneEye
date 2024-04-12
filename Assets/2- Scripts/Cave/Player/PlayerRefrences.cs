using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

[Serializable]
public class PlayerRefrences
{
    [SerializeField] private GameObject[] weaponObjects;

    [SerializeField] private GameObject barrelPrefab;

    [SerializeField] private Transform gunBarrel;
    
    [SerializeField] private AudioSource stepWalk;
    [SerializeField] private AudioSource death;
    
    [SerializeField] private GameObject gameOverUI;


    public GameObject[] WeaponObjects
    {
        get => weaponObjects;
        set => weaponObjects = value;
    }

    public Transform GunHandBarrel
    {
        get => gunBarrel;
        private set => gunBarrel = value;
    }

    public GameObject BarrelPrefab
    {
        get => barrelPrefab;
        private set => barrelPrefab = value;
    }
    
    public AudioSource StepWalk
    {
        get => stepWalk;
        private set => stepWalk = value;
    }
    
    public AudioSource Death
    {
        get => death;
        private set => death = value;
    }
    
    public GameObject GameOverUI
    {
        get => gameOverUI;
        set => gameOverUI = value;
    }
}
