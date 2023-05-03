using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsBehavior : MonoBehaviour
{
    private static ButtonsBehavior Instance { get; set;}
    private void Awake() 
    {
        Instance = this;    
    }
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void SceneLoad(int sceneId)
    {
        if(sceneId < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}
