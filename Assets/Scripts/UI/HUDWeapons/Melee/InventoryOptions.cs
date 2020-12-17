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
                InventoryMelee.Instance.Remove();
                
                Debug.Log("Sell: " + InventoryMelee.Instance.ChosenSlot);
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
