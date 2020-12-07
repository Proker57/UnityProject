using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        private string _stringTableCollectionName = "Item_names";

        // Names
        private string _potion_hp_small;
        private string _potion_hp_medium;
        private string _potion_hp_huge;

        // Description
        private string _potion_hp_small_description;
        private string _potion_hp_medium_description;
        private string _potion_hp_huge_description;

        public readonly int ItemId;

        public string Name;
        public string Description;
        public Sprite Sprite;

        public Item(int itemId)
        {
            ItemId = itemId;

            LoadStrings();
        }

        private void Init()
        {
            switch (ItemId)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    Sprite = ItemSprites.Instance.SmallPotion;
                    Name = _potion_hp_small;
                    Description = _potion_hp_small_description;
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    Sprite = ItemSprites.Instance.MediumPotion;
                    Name = _potion_hp_medium;
                    Description = _potion_hp_medium_description;
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    Sprite = ItemSprites.Instance.HugePotion;
                    Name = _potion_hp_huge;
                    Description = _potion_hp_huge_description;
                    break;
                default:

                    break;
            }
        }

        public void UseItem()
        {
            switch (ItemId)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    PlayerEvents.RestoreHealth(20);
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    PlayerEvents.RestoreHealth(50);
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    PlayerEvents.RestoreHealth(100);
                    break;
                default:
                    Debug.LogError("UseItem Method is not working");
                    break;
            }
        }

        public async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(_stringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                switch (ItemId)
                {
                    case (int)ItemEnum.ItemType.SmallPotion:
                        _potion_hp_small = GetLocalizedString(stringTable, "potion_hp_small");
                        _potion_hp_small_description = GetLocalizedString(stringTable, "potion_hp_small_description");
                        break;
                    case (int)ItemEnum.ItemType.MediumPotion:
                        _potion_hp_medium = GetLocalizedString(stringTable, "potion_hp_medium");
                        _potion_hp_medium_description = GetLocalizedString(stringTable, "potion_hp_medium_description");
                        break;
                    case (int)ItemEnum.ItemType.HugePotion:
                        _potion_hp_huge = GetLocalizedString(stringTable, "potion_hp_huge");
                        _potion_hp_huge_description = GetLocalizedString(stringTable, "potion_hp_huge_description");
                        break;
                    default:
                        Debug.LogError("Can't load ItemId Localization");
                        break;
                }

                Init();
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
