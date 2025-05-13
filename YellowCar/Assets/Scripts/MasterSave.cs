using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class MasterSave 
{
    private string _savePath = Application.dataPath + "/MySaves/PlayerData.json";
    public SaveData SaveData { get; private set; } = new SaveData();

    public void SaveAllData()
    {
        string jsonString = JsonConvert.SerializeObject(SaveData);
        File.WriteAllText(_savePath, jsonString);
    }

    public void LoadAllData()
    {
        if (File.Exists(_savePath))
        {
            string jsonString = File.ReadAllText(_savePath);
            SaveData = JsonConvert.DeserializeObject<SaveData>(jsonString);
        }
    }
}
