using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class ItemManager : MonoBehaviour, ISaveable
    {
        public List<Item> Items = new List<Item>();

        public int ItemIndex;

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            ItemIndex = -1;
            _playerInput = GetComponent<Player>().Input;
        }

        public void ItemPickUp(int itemId)
        {
            Items.Add(new Item(itemId));
        }

        private void ItemUse_started()
        {
            if (Items.Count > 0 && ItemIndex > -1)
            {
                Items[ItemIndex].UseItem();
                Items.Remove(Items[ItemIndex]);
                ItemIndex--;
            }
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

        private void OnEnable()
        {
            ItemEvents.ItemNext += NextItem;
            ItemEvents.ItemPickUp += ItemPickUp;
            _playerInput.PlayerInGame.ItemUse.started += _ => ItemUse_started();
        }

        private void OnDisable()
        {
            ItemEvents.ItemNext -= NextItem;
            ItemEvents.ItemPickUp -= ItemPickUp;
            _playerInput.PlayerInGame.ItemUse.started -= _ => ItemUse_started();
        }

        public object CaptureState()
        {
            return new ItemManagerData
            {
                Items = Items
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (ItemManagerData) state;

            Items = saveData.Items;
        }
    }

    [System.Serializable]
    public class ItemManagerData
    {
        public List<Item> Items;
    }
}
