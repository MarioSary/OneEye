using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RefGameObjectsEvent : MonoBehaviour
{
    public static RefGameObjectsEvent instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("3D World");
        trolleyUnchanged.Stop();
        trolleyChanged.Stop();
    }

    public void Switch()
    {
        StartCoroutine(Delay());
    }

    //for coding talking animation
    //define public AudioSource, Animator or any other variable here and then code for them in EventBeheviour...
    // public GameObject oneEye;
    // public GameObject fatMan;
    public GameObject NPCs;
    public Animator fatManAnimator;
    public List<GameObject> battleCameraTriggers;
    public List<GameObject> playerCameraTriggers;
    public List<SpriteRenderer> NPC;
    public GameObject battleBounds;
    public VideoPlayer trolleyUnchanged;
    public VideoPlayer trolleyChanged;
    public GameObject screenUnchanged;
    public GameObject screenChanged;
    public GameObject fatmanHealthCheck;


}
