using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour, ICollisionHandler
{
   [SerializeField] private string sceneToLoad;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log("NextScene");
         SceneManager.LoadScene(sceneToLoad);
         gameObject.SetActive(false);
      }
   }
   
   public void CollisionEnter(string colliderName, GameObject other)
   {
      if (colliderName == "NextScene" && other.tag == "Player")
      {
         Debug.Log("NextScene");
         SceneManager.LoadScene(sceneToLoad);
         gameObject.SetActive(false);
      }
      if (colliderName == "NextSceneChanged" && other.tag == "Player")
      {
         Debug.Log("NextScene");
         RefGameObjectsEvent.instance.screenChanged.SetActive(true);
         RefGameObjectsEvent.instance.trolleyChanged.Play();
         RefGameObjectsEvent.instance.Switch();
         gameObject.SetActive(false);
      }

   }

   public void CollisionExit(string colliderName, GameObject other)
   {
        
   }


   
}


   /*//GameManagment gameManagment;
public string sceneToLoad;

public void Awake()
{
   //gameManagment = FindObjectOfType<GameManagment>();
}

private void OnTriggerEnter(Collider other)
{
   // gameManagment.SaveData();
   // gameManagment.LoadNextLevel();
   SceneManager.LoadScene(sceneToLoad);
}*/

   /*//for loadingBar
public static AsyncLoader instance;
[SerializeField] private GameObject loadingScreen;
[SerializeField] private Image progressBar;
   
//for loadingBar
private void Awake()
{
   if (instance == null)
   {
      instance = this;
      DontDestroyOnLoad(gameObject);
   }
   else
   {
      Destroy(gameObject);
   }
}
private void Start()
{
   loadingScreen = GameObject.FindWithTag("LoadingCanvas");
}

public async void LoadBarScene(string sceneToLoad)
{
   Debug.Log("progressBar.fillAmount");
   var scene = SceneManager.LoadSceneAsync(sceneToLoad);
   scene.allowSceneActivation = false;

   loadingScreen.SetActive(true);

   do
   {
      await Task.Delay(100);
      progressBar.fillAmount = scene.progress;
   } while (scene.progress < 0.9f);


   scene.allowSceneActivation = true;
   loadingScreen.SetActive(false);
}*/