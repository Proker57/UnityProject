using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOYAREngine
{
    public class LoadManager
    {
        private const string FileName = "/Save.sosi";

        public void Load()
        {
            var dir = Application.persistentDataPath + FileName;
            if (File.Exists(dir) == false) return;
            var bf = new BinaryFormatter();
            var file = File.Open(dir, FileMode.Open);
            var saveData = (SaveManager.SaveData)bf.Deserialize(file);
            file.Close();

            LoadData(saveData);
        }

        public void LoadData(SaveManager.SaveData saveData)
        {
            if (Object.FindObjectOfType<Player>() == null) return;
            var stats = Object.FindObjectOfType<Player>().GetComponent<Stats>().PlayerData;

            stats.Health = saveData.Health;
            stats.XPosition = saveData.XPosition;
            stats.YPosition = saveData.YPosition;
        }
    }
}
