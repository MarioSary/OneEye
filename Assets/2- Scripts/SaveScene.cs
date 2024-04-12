using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveScene : MonoBehaviour, ICollisionHandler
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            Debug.Log("LevelSaved");
            gameObject.SetActive(false);
        }
    }
   
    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "SaveScene" && other.tag == "Player")
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            Debug.Log("LevelSaved");
            gameObject.SetActive(false);
        }

    }

    public void CollisionExit(string colliderName, GameObject other)
    {
        
    }
}
