using System;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance = null;

        [SerializeField] private const string EmptySlotSpritePath = "Images/UI/Inventory/Slot_Empty";

        public int ChosenSlot = -1;

        public enum TabType
        {
            Weapons, Items
        }

        public TabType CurrentTab = TabType.Weapons;

        [SerializeField] private GameObject _scrollView;
        public GameObject[] Cells;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void WeaponAddInInventory()
        {
            UpdateSprites(TabType.Weapons);
        }

        private void ItemAddInInventory()
        {
            UpdateSprites(TabType.Items);
        }

        public void Remove(TabType type)
        {
            switch (type)
            {
                case TabType.Weapons:
                    WeaponManager.Instance.Weapons.RemoveAt(ChosenSlot);

                    if (ChosenSlot <= WeaponManager.Instance.CurrentWeapon)
                    {
                        WeaponManager.Instance.CurrentWeapon--;
                    }
                    break;
                case TabType.Items:
                    ItemManager.Instance.Items.RemoveAt(ChosenSlot);

                    if (ChosenSlot <= ItemManager.Instance.CurrentItem)
                    {
                        ItemManager.Instance.CurrentItem--;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            UpdateSprites(type);
        }

        public void UpdateSprites(TabType type)
        {
            var i = 0;

            CurrentTab = type;

            foreach (var cell in Cells)
            {
                switch (type)
                {
                    case TabType.Weapons:
                        if (i < WeaponManager.Instance.Weapons.Count)
                        {
                            cell.GetComponent<Image>().sprite = WeaponManager.Instance.Weapons[i].SpriteUi;
                            i++;
                        }
                        else
                        {
                            cell.GetComponent<Image>().sprite = Resources.Load<Sprite>(EmptySlotSpritePath);
                            i++;
                        }
                        break;
                    case TabType.Items:
                        if (i < ItemManager.Instance.Items.Count)
                        {
                            cell.GetComponent<Image>().sprite = ItemManager.Instance.Items[i].SpriteUi;
                            i++;
                        }
                        else
                        {
                            cell.GetComponent<Image>().sprite = Resources.Load<Sprite>(EmptySlotSpritePath);
                            i++;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }

        private void OnEnable()
        {
            WeaponEvents.WeaponAddInInventory += WeaponAddInInventory;
            ItemEvents.ItemAddInInventory += ItemAddInInventory;
        }

        private void OnDisable()
        {
            WeaponEvents.WeaponAddInInventory -= WeaponAddInInventory;
            ItemEvents.ItemAddInInventory -= ItemAddInInventory;
        }
    }
}
