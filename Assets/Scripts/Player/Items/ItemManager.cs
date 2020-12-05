using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class ItemManager : MonoBehaviour, ISaveable
    {
        public static ItemManager Instance = null;

        public List<Item> Items = new List<Item>();

        public int ItemIndex;

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

        public void ItemPickUp(int itemId)
        {
            Items.Add(new Item(itemId));
        }

        private void ItemUse_started()
        {
            if (Items.Count <= 0 || ItemIndex <= -1) return;
            Items[ItemIndex].UseItem();
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
        }

        private void OnDisable()
        {
            ItemEvents.ItemNext -= NextItem;
            ItemEvents.ItemPrevious -= PreviousItem;
            ItemEvents.ItemPickUp -= ItemPickUp;

            _playerInput.PlayerInGame.ItemUse.started -= _ => ItemUse_started();
            _playerInput.PlayerInGame.NextItem.started -= _ => NextItem_started();
            _playerInput.PlayerInGame.PreviousItem.started -= _ => PreviousItem_started();
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
    }

    [System.Serializable]
    public class ItemManagerData
    {
        public List<Item> Items;
        public int ItemIndex;
    }
}
