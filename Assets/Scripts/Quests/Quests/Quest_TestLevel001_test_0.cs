using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine.Quests
{
    [System.Serializable]
    public class Quest_TestLevel001_test_0 : Task
    {
        public int KillEnemyCurrent = 0;
        public int KillEnemyNeed = 1;

        public Quest_TestLevel001_test_0()
        {
            Id = "Quest_TestLevel001_test_0";
            Name = "Test Quest1";
            Description = "Quest description";
            IsFinished = false;

            LoadStrings();

            OnEnable();
        }

        internal override void Finish()
        {
            OnDisable();
            PlayerEvents.GiveCurrency(100);
            QuestManager.Instance.UpdateQuestList(Id);
        }

        private void OnKillEnemy()
        {
            KillEnemyCurrent++;

            if (KillEnemyCurrent < KillEnemyNeed) return;
            IsFinished = true;
            QuestManager.Instance.UpdateCells();
        }

        private void OnEnable()
        {
            KillEvents.KIllEnemy += OnKillEnemy;
        }

        public void OnDisable()
        {
            KillEvents.KIllEnemy -= OnKillEnemy;
        }

        internal sealed override async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                Name = GetLocalizedString(stringTable, "quest_TestLevel001_test_0_name");
                Description = GetLocalizedString(stringTable, "quest_TestLevel001_test_0_description");

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
