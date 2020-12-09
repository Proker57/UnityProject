using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class ItemManager : MonoBehaviour, ISaveable
    {
        public static ItemManager Instance = null;

        public int ItemIndex;
        public List<Item> Items = new List<Item>();

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            ItemIndex = -1;
            _playerInput = GetComponent<Player>().Input;
        }

        public void ItemPickUp(Item item)
        {
            Items.Add(item);
        }

        private void ItemUse_started()
        {
            if (Items.Count <= 0 || ItemIndex <= -1) return;
            Items[ItemIndex].Use();
            Items.Remove(Items[ItemIndex]);
            ItemIndex--;
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
            if (Items != null && ItemIndex >= Items.Count - 1)
            {
                ItemIndex = -1;
            }
            else
            {
                ItemIndex++;
            }
        }

        private void PreviousItem()
        {
            if (Items != null && ItemIndex <= -1)
            {
                ItemIndex = Items.Count - 1;
            }
            else
            {
                ItemIndex--;
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
                ItemIndex = ItemIndex
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (ItemManagerData) state;

            Items = saveData.Items;
            ItemIndex = saveData.ItemIndex;
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
