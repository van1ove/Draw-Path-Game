using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class ButtonsChecker : MonoBehaviour
{
    public static ButtonsChecker Instance {get; private set;} 
    [SerializeField] private List<Button> buttons = new List<Button>();

    private void Awake() 
    {
        Instance = this;
    }
    private void Start() 
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = false;
        }
        SaveManager.Instance.Load();
        CheckAvailableLevels();
    }
    private void CheckAvailableLevels()
    {
        for(int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if(i - 1 <= SaveManager.Instance.LevelsOpened)
            {
                buttons[i - 2].interactable = true;
            }
            else return;
        }
    }


}
