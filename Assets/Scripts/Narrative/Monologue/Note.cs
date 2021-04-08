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
        public int Count;
        public float[] WaitTimer;
        public float WaitMultiplier = 0.2f;

        public bool IsLoaded;

        private string _id;

        public Note(string id)
        {
            _id = id;

            LoadStrings();
        }

        private async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                Text = GetLocalizedString(stringTable, _id).Split('\n');
                WaitTimer = new float[Text.Length];
                Count = Text.Length;

                for (var i = 0; i < Text.Length; i++)
                {
                    WaitTimer[i] = Text[i].Length * WaitMultiplier;
                }

                IsLoaded = true;
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

