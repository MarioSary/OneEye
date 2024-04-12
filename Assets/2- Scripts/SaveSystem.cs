using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    public string playerHealthKey = "PlayerHealth", scenekey = "SceneIndex", savePresentKey = "SavePresent";
    public LoadedData loadedData { get; private set; }
    public UnityEvent<bool> OnDataLoadedResult;
    private bool isinitializes = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (isinitializes)
            return;
        var result = LoadData();
        OnDataLoadedResult?.Invoke(result);
        isinitializes = true;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(playerHealthKey);
        PlayerPrefs.DeleteKey(scenekey);
        PlayerPrefs.DeleteKey(savePresentKey);
        loadedData = null;
    }

    public bool LoadData()
    {
        if (PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            loadedData = new LoadedData();
            loadedData.playerHealth = PlayerPrefs.GetInt(playerHealthKey);
            loadedData.sceneIndex = PlayerPrefs.GetInt(scenekey);
            return true;
        }

        return false;
    }

    public void SaveData(int sceneIndex, int playerHealth)
    {
        if (loadedData == null)
        {
            loadedData = new LoadedData();
            loadedData.playerHealth = playerHealth;
            loadedData.sceneIndex = sceneIndex;
            PlayerPrefs.SetInt(playerHealthKey, playerHealth);
            PlayerPrefs.SetInt(scenekey, sceneIndex);
            PlayerPrefs.SetInt(savePresentKey, 1);
        }
    }
}

public class LoadedData
{
    public int playerHealth = -1;
    public int sceneIndex = -1;
}
