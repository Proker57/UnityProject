using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class InventoryOptions : MonoBehaviour
    {
        public bool IsActive;

        public void Sell()
        {
            if (!IsActive) return;
            var chosenSlot = Inventory.Instance.ChosenSlot;

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    var weaponsCount = WeaponManager.Instance.Weapons.Count - 1;

                    if (chosenSlot == WeaponManager.Instance.CurrentWeapon)
                    {
                        WeaponManager.Instance.SetWeapon(-1);
                    }

                    if (chosenSlot > weaponsCount) return;
                    PlayerEvents.GiveCurrency(WeaponManager.Instance.Weapons[chosenSlot].SellCost);
                    break;
                case Inventory.TabType.Items:
                    var itemsCount = ItemManager.Instance.Items.Count - 1;

                    if (chosenSlot == ItemManager.Instance.CurrentItem)
                    {
                        ItemManager.Instance.SetItem(-1);
                    }

                    if (chosenSlot > itemsCount) return;
                    PlayerEvents.GiveCurrency(ItemManager.Instance.Items[chosenSlot].SellCost);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Inventory.Instance.Remove(Inventory.Instance.CurrentTab);
            Inventory.Instance.ChosenSlot--;
            HighlightChosenWeapon();

            IsActive = false;
        }

        public void Sort()
        {
            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    if (WeaponManager.Instance.Weapons.Count <= 0) return;
                    WeaponManager.Instance.Weapons =
                        WeaponManager.Instance.Weapons.OrderBy(x => x.Damage).ToList();
                    break;
                case Inventory.TabType.Items:
                    if (ItemManager.Instance.Items.Count <= 0) return;
                    ItemManager.Instance.Items =
                        ItemManager.Instance.Items.OrderBy(x => x.Type).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);
            HighlightChosenWeapon();
        }

        public void Equip()
        {
            if (!IsActive) return;

            var chosenSlot = Inventory.Instance.ChosenSlot;

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    if (chosenSlot <= WeaponManager.Instance.Weapons.Count)
                    {
                        WeaponManager.Instance.SetWeapon(chosenSlot);
                        HighlightChosenWeapon();
                    }
                    break;
                case Inventory.TabType.Items:
                    if (chosenSlot <= ItemManager.Instance.Items.Count)
                    {
                        ItemManager.Instance.Items[chosenSlot].Use();
                        Inventory.Instance.Remove(Inventory.Instance.CurrentTab);
                        Inventory.Instance.ChosenSlot--;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IsActive = false;
        }

        public static void HighlightChosenWeapon()
        {
            ClearHighLight();

            var currentWeapon = WeaponManager.Instance.CurrentWeapon;
            if (currentWeapon != -1) Inventory.Instance.Cells[currentWeapon].GetComponent<Image>().color = Color.red;
        }

        public static void ClearHighLight()
        {
            foreach (var cell in Inventory.Instance.Cells)
            {
                cell.GetComponent<Image>().color = Color.white;
            }
        }

        public void EnterPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = true;
        }
    }
}
