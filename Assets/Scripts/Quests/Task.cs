using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine.Quests
{
    [System.Serializable]
    public class Task
    {
        internal const string StringTableCollectionName = "Quests";

        [SerializeField] internal string Id;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;
        [SerializeField] internal bool IsFinished;

        internal virtual void Finish() { }

        internal virtual async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                var splitText = GetLocalizedString(stringTable, Id).Split('\n');

                Name = splitText[0];
                Description = splitText[1];

                QuestManager.Instance.UpdateCells();
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
