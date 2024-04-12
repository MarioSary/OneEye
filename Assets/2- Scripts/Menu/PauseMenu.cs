using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] public AudioSource backMusic;

    private void Start()
    {
        GameIsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            if(GameIsPaused)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        backMusic.Pause();
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        backMusic.UnPause();
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioListener.volume = 1;
    }

    public void Option()
    {
        
    }


}
