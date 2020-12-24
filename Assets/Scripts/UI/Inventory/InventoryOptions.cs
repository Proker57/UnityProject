using System;
using System.Linq;
using UnityEngine;

namespace BOYAREngine
{
    public class InventoryOptions : MonoBehaviour
    {
        public bool IsActive;

        public void Sell()
        {
            if (!IsActive) return;
            var chosenSlot = Inventory.Instance.ChosenSlot;

            if (Inventory.Instance.CurrentTab == Inventory.TabType.Weapons)
            {

            }

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    var weaponsCount = WeaponManager.Instance.Weapons.Count - 1;

                    if (chosenSlot == WeaponManager.Instance.CurrentWeapon)
                    {
                        WeaponManager.Instance.SetWeapon(-1);
                        return;
                    }

                    if (chosenSlot > weaponsCount) return;

                    PlayerEvents.GiveCurrency(WeaponManager.Instance.Weapons[chosenSlot].SellCost);
                    break;
                case Inventory.TabType.Items:
                    var itemsCount = ItemManager.Instance.Items.Count - 1;

                    if (chosenSlot == ItemManager.Instance.CurrentItem)
                    {
                        ItemManager.Instance.SetItem(-1);
                        return;
                    }

                    if (chosenSlot > itemsCount) return;
                    PlayerEvents.GiveCurrency(ItemManager.Instance.Items[chosenSlot].SellCost);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Inventory.Instance.Remove(Inventory.Instance.CurrentTab);
            Inventory.Instance.ChosenSlot--;

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

        }

        public void Equip()
        {
            if (!IsActive) return;

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    if (Inventory.Instance.ChosenSlot <= WeaponManager.Instance.Weapons.Count)
                    {
                        WeaponManager.Instance.SetWeapon(Inventory.Instance.ChosenSlot);
                    }
                    break;
                case Inventory.TabType.Items:
                    if (Inventory.Instance.ChosenSlot <= ItemManager.Instance.Items.Count)
                    {
                        ItemManager.Instance.Items[Inventory.Instance.ChosenSlot].Use();
                        Inventory.Instance.Remove(Inventory.Instance.CurrentTab);
                        Inventory.Instance.ChosenSlot--;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IsActive = false;
        }

        public void EnterPointer()
        {
            // TODO change input scheme
            Player.Instance.Input.Disable();
        }

        public void ExitPointer()
        {
            Player.Instance.Input.Enable();
        }
    }
}
