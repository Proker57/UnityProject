using UnityEngine;

namespace BOYAREngine.UI
{
    public class Sell : MonoBehaviour
    {
        private const string EmptySlotSpritePath = "Images/UI/Inventory/Slot_Empty";

        [SerializeField] private WeaponsTab _weaponsTab;

        public void OnClick()
        {
            if (WeaponManager.Instance.CurrentWeapon != -1)
            {
                _weaponsTab.Cells[WeaponManager.Instance.CurrentWeapon].sprite =
                    Resources.Load<Sprite>(EmptySlotSpritePath);

                PlayerEvents.GiveCurrency?.Invoke(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].SellCost);

                WeaponManager.Instance.Weapons.RemoveAt(WeaponManager.Instance.CurrentWeapon);
                WeaponManager.Instance.CurrentWeapon = -1;

            }

        }
    }
}

