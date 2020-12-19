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
            var weaponsCount = WeaponManager.Instance.Weapons.Count - 1;
            if (chosenSlot > weaponsCount) return;
            PlayerEvents.GiveCurrency(WeaponManager.Instance.Weapons[chosenSlot].SellCost);
            Inventory.Instance.Remove();
            if (chosenSlot > weaponsCount)
            {
                Inventory.Instance.ChosenSlot--;
            }
        }

        public void Sort()
        {
            if (WeaponManager.Instance.Weapons.Count <= 0) return;
            WeaponManager.Instance.Weapons
                = WeaponManager.Instance.Weapons.OrderBy(x => x.Damage).ToList();
            Inventory.Instance.UpdateSprites();

        }

        public void Equip()
        {
            if (!IsActive) return;
            if (Inventory.Instance.ChosenSlot <= WeaponManager.Instance.Weapons.Count)
            {
                WeaponManager.Instance.CurrentWeapon = Inventory.Instance.ChosenSlot;
            }
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
