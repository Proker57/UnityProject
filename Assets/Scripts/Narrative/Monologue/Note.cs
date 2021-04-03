using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine.Narrative
{
    [System.Serializable]
    public class Note
    {
        internal const string StringTableCollectionName = "Notes";

        public string[] Text;
        public float[] WaitTimer;
        public int Count;
        private string _id;

        public Note(string id, int count)
        {
            Text = new string[count];
            WaitTimer = new float[count];
            Count = count;
            _id = id;

            LoadStrings();
        }

        internal async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                for (var i = 0; i < Text.Length; i++)
                {
                    Text[i] = GetLocalizedString(stringTable, _id + "_" + i);
                    WaitTimer[i] = Text[i].Length * 0.2f;
                }

                MonologueEvents.LoadedMonologue?.Invoke();
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
            }
        }

        private static string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}

