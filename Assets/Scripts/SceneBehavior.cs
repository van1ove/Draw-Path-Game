using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class SceneBehavior : MonoBehaviour
{
    public static SceneBehavior Instance { get; private set; }
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start() 
    {
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        failedPanel.SetActive(false);    
        levelText.SetText("Level " + (SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void PlayersWon()
    {
        winPanel.SetActive(true);
        //int level = PlayerPrefs.GetInt("Level");
        Debug.Log(SaveManager.Instance.LevelsOpened);
        if(SaveManager.Instance.LevelsOpened == SceneManager.GetActiveScene().buildIndex - 1) SaveManager.Instance.LevelsOpened++;
        Debug.Log(SaveManager.Instance.LevelsOpened);
        
        SaveManager.Instance.Save();
        
        // PlayerPrefs.SetInt("Level", level);
        // PlayerPrefs.Save();
    }
    public void PlayersFailed()
    {
        failedPanel.SetActive(true);
    }
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSelectionScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            LoadSelectionScene();
        }
    }
}
