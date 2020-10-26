using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOYAREngine
{
    public class SaveManager
    {
        private const string FileName = "/Save.sosi";

        public void Save(PlayerData data)
        {
            var dir = Application.persistentDataPath + FileName;

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
            save.XPosition = data.XPosition;
            save.YPosition = data.YPosition;

            save.Health = data.Health;
            save.MaxHealth = data.MaxHealth;
            save.Level = data.Level;
            save.EXP = data.EXP;
        }

        [System.Serializable]
        public class SaveData
        {
            // TODO Do save better
            public float XPosition;
            public float YPosition;

            public int Health;
            public int MaxHealth;
            public int Level;
            public int EXP;
        }
    }
}
