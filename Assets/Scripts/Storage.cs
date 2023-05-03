using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Storage
{
    private readonly string _filePath;
    private BinaryFormatter _formatter;
    public Storage()
    {
        var directory = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        _filePath = directory + "/GameSave.save"; 
        
        Debug.Log(_filePath);
        InitFormatter();
    }

    private void InitFormatter()
    { 
        _formatter = new BinaryFormatter();
        var selector = new SurrogateSelector();
        _formatter.SurrogateSelector = selector;
    }

    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(_filePath))
        {
            if (saveDataByDefault != null) Save(saveDataByDefault);
            return saveDataByDefault;
        }

        var file = File.Open(_filePath, FileMode.Open);
        var savedData = _formatter.Deserialize(file);
        file.Close();
        return savedData;
    }

    public void Save(object saveData)
    {
        var file = File.Create(_filePath);
        _formatter.Serialize(file, saveData);
        file.Close();
    }
    
}
