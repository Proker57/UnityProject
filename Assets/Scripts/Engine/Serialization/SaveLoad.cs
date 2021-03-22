using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class SaveLoad : MonoBehaviour
    {
        private string SavePath => $"{Application.persistentDataPath}/save.sosi";

        public Text SaveText;
        public Text LoadText;

        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);

            Events.Save?.Invoke();

            ShowSaveText();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            GameController.IsNewGame = false;
            RestoreState(state);

            Events.Load?.Invoke();

            ShowLoadText();
        }

        private void ShowSaveText()
        {
            SaveText.enabled = true;
            Invoke("SaveTextDisable", 2f);
        }

        private void SaveTextDisable()
        {
            SaveText.enabled = false;
        }

        private void ShowLoadText()
        {
            LoadText.enabled = true;
            Invoke("LoadTextDisable", 2f);
        }

        private void LoadTextDisable()
        {
            LoadText.enabled = false;
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>) formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}
