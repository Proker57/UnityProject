
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class ItemPotionHpSmall : Item
    {
        [SerializeField] private int _hpRestoreAmount = 20;

        public ItemPotionHpSmall()
        {
            Sprite = ItemSprites.Instance.SmallPotion;

            LoadStrings();
        }

        internal override void Use()
        {
            PlayerEvents.RestoreHealth(_hpRestoreAmount);
        }

        internal sealed override async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                Name = GetLocalizedString(stringTable, "potion_hp_small");
                Description = GetLocalizedString(stringTable, "potion_hp_small_description");
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
