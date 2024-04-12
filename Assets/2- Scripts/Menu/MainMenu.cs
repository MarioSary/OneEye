using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

   [Header("Volume Setting")] 
   [SerializeField] private Slider volumeSlider;

   [Header("Graphics Setting")] 
   [SerializeField] private Slider brightnessSlider = null;

   private int qualityLevel;
   private bool isFullScreen = false;
   private float brightnessLevel;

   [Header("Resolution Dropdown")] 
   public TMP_Dropdown resolutionDropdown;
   
   private Resolution[] resolutions;
   

   [Header("Levels To Load")] 
   public string newGameLevel;

   private string levelToLoad;
   [SerializeField] private GameObject noSavedGamePopUp = null;
   [SerializeField] private GameObject mainMenuPanel;

   private void Start()
   {
      resolutions = Screen.resolutions;
      resolutionDropdown.ClearOptions();

      List<string> options = new List<string>();
      int currentResolutionIndex = 0;

      for (int i = 0; i < resolutions.Length; i++)
      {
         string option = resolutions[i].width + "x" + resolutions[i].height;
         options.Add(option);

         if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
         {
            currentResolutionIndex = i;
         }
      }
      
      resolutionDropdown.AddOptions(options);
      resolutionDropdown.value = currentResolutionIndex;
      resolutionDropdown.RefreshShownValue();
   }

   public void SetResolution(int resolutionIndex)
   {
      Resolution resolution = resolutions[resolutionIndex];
      Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
   }

   public void NewGameYes()
   {
      PlayerPrefs.DeleteKey("LevelSaved");
      SceneManager.LoadScene(newGameLevel);
   }

   public void ContinueYes()
   {
      if (PlayerPrefs.HasKey("LevelSaved"))
      {
         levelToLoad = PlayerPrefs.GetString("LevelSaved");
         SceneManager.LoadScene(levelToLoad);
      }
      else
      {
         noSavedGamePopUp.SetActive(true);
      }
   }

   public void DeactiveButton()
   {
      foreach (var btn in mainMenuPanel.GetComponentsInChildren<Button>(true))
      {
         btn.interactable = false;
      }
   }
   
   public void ActiveButton()
   {
      foreach (var btn in mainMenuPanel.GetComponentsInChildren<Button>(false))
      {
         btn.interactable = true;
      }
   }

   public void SetVolume(float volume)
   {
      Debug.Log(volume);
      AudioListener.volume = volume;
   }

   public void VolumeApply()
   {
      PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
      //StartCoroutine(confirmationBox());
   }

   // public IEnumerator confirmationBox()
   // {
   //    confirmationPrompt.SetActive(true);
   //    yield return new WaitForSeconds(2);
   //    confirmationPrompt.SetActive(false);
   // }

   public void SetBrightness(float brightness)
   {
      brightnessLevel = brightness;
   }

   public void SetFullScreen(bool isFullscreen)
   {
      isFullScreen = isFullscreen;
   }

   public void SetQuality(int qualityIndex)
   {
      qualityLevel = qualityIndex;
   }

   public void GraphicsApply()
   {
      PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);
      //change your brightness with your post proccessing or whatever it is
      
      PlayerPrefs.SetInt("masterQuality", qualityLevel);
      QualitySettings.SetQualityLevel(qualityLevel);
      
      PlayerPrefs.SetInt("masterFullscreen", (isFullScreen ? 1 : 0));
      Screen.fullScreen = isFullScreen;
   }

   public void ExitGame()
   {
      Debug.Log("Exit Done");
      Application.Quit();
   }
}
