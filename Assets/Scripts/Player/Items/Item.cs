using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        private const string StringTableCollectionName = "Item_names";

        public readonly int ItemId;

        public string Name;
        public string Description;
        public Sprite Sprite;

        // Names
        private string _potionHpSmall;
        private string _potionHpMedium;
        private string _potionHpHuge;

        // Description
        private string _potionHpSmallDescription;
        private string _potionHpMediumDescription;
        private string _potionHpHugeDescription;

        public Item(int itemId)
        {
            ItemId = itemId;

            LoadStrings();
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

        public virtual void Use()
        {

        } 

        public async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                switch (ItemId)
                {
                    case (int)ItemEnum.ItemType.SmallPotion:
                        _potionHpSmall = GetLocalizedString(stringTable, "potion_hp_small");
                        _potionHpSmallDescription = GetLocalizedString(stringTable, "potion_hp_small_description");
                        break;
                    case (int)ItemEnum.ItemType.MediumPotion:
                        _potionHpMedium = GetLocalizedString(stringTable, "potion_hp_medium");
                        _potionHpMediumDescription = GetLocalizedString(stringTable, "potion_hp_medium_description");
                        break;
                    case (int)ItemEnum.ItemType.HugePotion:
                        _potionHpHuge = GetLocalizedString(stringTable, "potion_hp_huge");
                        _potionHpHugeDescription = GetLocalizedString(stringTable, "potion_hp_huge_description");
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

        private void Init()
        {
            switch (ItemId)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    InitData(ItemSprites.Instance.SmallPotion, _potionHpSmall, _potionHpSmallDescription);
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    InitData(ItemSprites.Instance.MediumPotion, _potionHpMedium, _potionHpMediumDescription);
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    InitData(ItemSprites.Instance.HugePotion, _potionHpHuge, _potionHpHugeDescription);
                    break;
                default:
                    Debug.LogError("There are no item with this ID");
                    break;
            }
        }

        private void InitData(Sprite sprite, string name, string description)
        {
            Sprite = sprite;
            Name = name;
            Description = description;
        }
    }

    public class ItemPotionSmall : Item
    {
        public override void Use()
        {
            PlayerEvents.RestoreHealth(20);
        }

        public ItemPotionSmall(int itemId) : base(itemId)
        {
        }
    }
}
