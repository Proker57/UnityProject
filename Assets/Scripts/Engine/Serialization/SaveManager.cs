using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    const string fileName = "/Save.sosi";
    public static event Events.SaveEvent SaveEvent;

    public void Save()
    {
        SaveEvent?.Invoke();

        var dir = Application.persistentDataPath + fileName;
        Debug.Log(Application.persistentDataPath);

        var bf = new BinaryFormatter();
        var file = File.Open(dir, FileMode.OpenOrCreate);
        var saveData = new SaveData();

        // TODO Save array of objects
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("File closed");
    }

    [System.Serializable]
    public class SaveData
    {
        // TODO Do save better
        public int Health = 1;
    }
}