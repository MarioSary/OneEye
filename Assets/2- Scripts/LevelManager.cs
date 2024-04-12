using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   /*[SerializeField] private string newGameLevel;

   public void NewGameButton()
   {
      SceneManager.LoadScene(newGameLevel);
   }

   public void ContinueGameButton()
   {
      if (PlayerPrefs.HasKey("LevelSaved"))
      {
         string levelToLoad = PlayerPrefs.GetString("LevelSaved");
         SceneManager.LoadScene(levelToLoad);
      }
   }*/


   /*public static GameManagment instance;
   public GameObject player;
   public SaveSystem saveSystem;

   private void Awake()
   {
      SceneManager.sceneLoaded += Initialize;
      DontDestroyOnLoad(gameObject);
   }

   private void Initialize(Scene scene, LoadSceneMode sceneMode)
   {
      Debug.Log("Loaded GM");
      var Player = FindObjectOfType<Player>();
      if (Player != null)
         player = Player.gameObject;
      saveSystem = FindObjectOfType<SaveSystem>();
      if (player != null && saveSystem.loadedData != null)
      {
         var damagable = Player.Stats.Lives;
         Player.Stats.Lives = saveSystem.loadedData.playerHealth;
      }
   }

   public void LoadLevel()
   {
      if (saveSystem.loadedData != null)
      {
         SceneManager.LoadScene(saveSystem.loadedData.sceneIndex);
         return;
      }

      LoadNextLevel();
   }

   public void LoadNextLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void SaveData()
   {
      if (player != null)
      {
         saveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1, PlayerStats.instance.Lives);
      }
   }*/
}
