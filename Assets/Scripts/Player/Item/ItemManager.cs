using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class ItemManager : MonoBehaviour, ISaveable
    {
        public static ItemManager Instance = null;

        public int CurrentItem = 1;

        public List<Item> Items = new List<Item>();

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _playerInput = GetComponent<Player>().Input;
        }

        public void ItemPickUp(Item item)
        {
            Items.Add(item);

            ItemEvents.ItemAddInInventory();
        }

        public void SetItem(int itemIndex)
        {
            CurrentItem = itemIndex;
        }

        private void ItemUse_started()
        {
            if (Items.Count <= 0 || CurrentItem <= -1) return;
            Items[CurrentItem].Use();
            Items.Remove(Items[CurrentItem]);
            CurrentItem--;
        }

        private void NextItem_started()
        {
            NextItem();
        }

        private void PreviousItem_started()
        {
            PreviousItem();
        }

        private void NextItem()
        {
            if (Items != null && CurrentItem >= Items.Count - 1)
            {
                CurrentItem = -1;
            }
            else
            {
                CurrentItem++;
            }
        }

        private void PreviousItem()
        {
            if (Items != null && CurrentItem <= -1)
            {
                CurrentItem = Items.Count - 1;
            }
            else
            {
                CurrentItem--;
            }
        }

        private void OnEnable()
        {
            ItemEvents.ItemNext += NextItem;
            ItemEvents.ItemPrevious += PreviousItem;
            ItemEvents.ItemPickUp += ItemPickUp;

            _playerInput.PlayerInGame.ItemUse.started += _ => ItemUse_started();
            _playerInput.PlayerInGame.NextItem.started += _ => NextItem_started();
            _playerInput.PlayerInGame.PreviousItem.started += _ => PreviousItem_started();

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            ItemEvents.ItemNext -= NextItem;
            ItemEvents.ItemPrevious -= PreviousItem;
            ItemEvents.ItemPickUp -= ItemPickUp;

            _playerInput.PlayerInGame.ItemUse.started -= _ => ItemUse_started();
            _playerInput.PlayerInGame.NextItem.started -= _ => NextItem_started();
            _playerInput.PlayerInGame.PreviousItem.started -= _ => PreviousItem_started();

            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        public object CaptureState()
        {
            return new ItemManagerData
            {
                Items = Items,
                ItemIndex = CurrentItem
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (ItemManagerData) state;

            Items = saveData.Items;
            CurrentItem = saveData.ItemIndex;

            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            foreach (var item in Items)
            {
                item.LoadStrings();
            }
        }
    }

    [System.Serializable]
    public class ItemManagerData
    {
        public List<Item> Items;
        public int ItemIndex;
    }
}
