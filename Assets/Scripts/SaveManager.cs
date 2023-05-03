using System;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager: MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private Storage _storage;
    private LevelData _level;
    public int LevelsOpened { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _storage = new Storage();
        _level = (LevelData) _storage.Load(new LevelData());
        Debug.Log("loaded, " + _level.LevelsPassed);
        LevelsOpened = _level.LevelsPassed;
    }
    
    public void Save()
    {
        _level.LevelsPassed = LevelsOpened;
        _storage.Save(_level);
        Debug.Log("saved"); 
    }

    public void Load()
    {
        _level = (LevelData)_storage.Load(new LevelData());
        LevelsOpened = _level.LevelsPassed;
        Debug.Log("opened: " + LevelsOpened);
    }
}

