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
                WeaponManager.Instance.SetWeapon(-1);

                UpdateSprites();
            }
        }

        private void UpdateSprites()
        {
            for (var i = 0; i < WeaponManager.Instance.Weapons.Count; i++)
            {
                _weaponsTab.Cells[i].sprite = Resources.Load<Sprite>(WeaponManager.Instance.Weapons[i].SpriteUi);
            }

            _weaponsTab.Cells[WeaponManager.Instance.Weapons.Count].sprite = Resources.Load<Sprite>(EmptySlotSpritePath);
        }
    }
}

