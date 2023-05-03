using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneBehavior : MonoBehaviour
{
    public static SceneBehavior Instance { get; private set; }
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start() 
    {
        Time.timeScale = 1f;
        _winPanel.SetActive(false);
        _failedPanel.SetActive(false);    
        _levelText.SetText("Level " + (SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void PlayersWon()
    {
        _winPanel.SetActive(true);
        int level = PlayerPrefs.GetInt("Level");
        if(level == SceneManager.GetActiveScene().buildIndex - 1) level++;
        
        PlayerPrefs.SetInt("Level", level);
    }
    public void PlayersFailed()
    {
        _failedPanel.SetActive(true);
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
