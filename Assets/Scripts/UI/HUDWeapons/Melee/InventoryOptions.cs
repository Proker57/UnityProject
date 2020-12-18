using UnityEngine;

namespace BOYAREngine
{
    public class InventoryOptions : MonoBehaviour
    {
        public bool IsActive;

        public void Sell()
        {
            if (IsActive)
            {
                PlayerEvents.GiveCurrency(WeaponManager.Instance.MeleeWeapons[InventoryMelee.Instance.ChosenSlot].SellCost);
                InventoryMelee.Instance.Remove();
                
                Debug.Log("Sell: " + InventoryMelee.Instance.ChosenSlot + "_ for: " + WeaponManager.Instance.MeleeWeapons[InventoryMelee.Instance.ChosenSlot].SellCost);
            }
        }

        public void Equip()
        {
            if (IsActive)
            {
                WeaponManager.Instance.CurrentWeapon = InventoryMelee.Instance.ChosenSlot;
            }
        }
    }
}
