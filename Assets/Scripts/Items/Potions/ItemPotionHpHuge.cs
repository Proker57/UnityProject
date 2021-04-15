using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class ItemPotionHpHuge : Item
    {
        [SerializeField] private int _hpRestoreAmount = 100;

        public ItemPotionHpHuge()
        {
            //SpriteUi = Resources.Load<Sprite>("Images/Items/Potions/Huge");
            //Sprite = Resources.Load<Sprite>("Images/Items/Potions/Huge");

            SpriteUi = "Images/Items/Potions/Huge";
            Sprite = "Images/Items/Potions/Huge";

            Name = "Huge potion";
            Type = "Potion";
            SellCost = 10;

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

                Type = GetLocalizedString(stringTable, "type_potion");
                Name = GetLocalizedString(stringTable, "potion_hp_huge");
                Description = GetLocalizedString(stringTable, "potion_hp_huge_description");
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