using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOYAREngine
{
    public class LoadManager
    {
        const string fileName = "/Save.sosi";

        public void Load()
        {
            if (File.Exists(fileName) == true)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(fileName, FileMode.Open);
                SaveManager.SaveData saveData = (SaveManager.SaveData)bf.Deserialize(file);
                file.Close();

                LoadData(saveData);
                Debug.Log("Loaded");
            }
        }

        public void LoadData(SaveManager.SaveData saveData)
        {
            //Stats stats = new Stats();
            //stats.Health = SaveManager.SaveData.Health;
        }
    }
}
