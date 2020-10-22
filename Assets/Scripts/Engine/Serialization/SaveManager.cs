using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOYAREngine
{
    public class SaveManager
    {
        const string fileName = "/Save.sosi";

        public void Save(PlayerData data)
        {
            var dir = Application.persistentDataPath + fileName;

            var bf = new BinaryFormatter();
            var file = File.Open(dir, FileMode.OpenOrCreate);
            var saveData = new SaveData();
            FillData(saveData, data);

            // TODO Save array of objects
            bf.Serialize(file, saveData);
            file.Close();
        }

        private void FillData(SaveData save, PlayerData data)
        {
            save.Health = data.Health;
        }

        [System.Serializable]
        public class SaveData
        {
            // TODO Do save better
            public int Health;
        }
    }
}
