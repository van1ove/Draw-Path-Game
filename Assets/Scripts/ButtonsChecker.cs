using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonsChecker : MonoBehaviour
{
    public static ButtonsChecker Instance {get; private set;}
    [SerializeField] private List<Button> _buttons = new List<Button>();
    private void Awake() 
    {
        Instance = this;    
    }
    private void Start() 
    {
        for(int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].interactable = false;
        }
        CheckAvailableLevels();
    }
    private void CheckAvailableLevels()
    {
        for(int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if(i - 1 > PlayerPrefs.GetInt("Level")) return;
             _buttons[i - 2].interactable = true;
        }
    }
}
