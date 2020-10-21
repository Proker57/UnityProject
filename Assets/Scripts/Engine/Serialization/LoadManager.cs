using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadManager
{
    const string fileName = "/Save.sosi";

    public void Load()
    {
        if (File.Exists(fileName) == true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open( fileName, FileMode.Open);
            SaveManager.SaveData saveData = (SaveManager.SaveData) bf.Deserialize(file);
            file.Close();

            RestoreSaveData(saveData);
            Debug.Log("Loaded");
        }
    }

    private void RestoreSaveData(SaveManager.SaveData saveData)
    {
        Stats stats = new Stats();

        stats.Health = saveData.Health;
    }
}
