using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    private int _levelsPassed;

    public int LevelsPassed
    {
        get => _levelsPassed;
        set => _levelsPassed = value;
    }

    public LevelData()
    {
        LevelsPassed = 1;
    }
}
