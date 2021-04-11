using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine.UI
{
    public class WeaponsTab : MonoBehaviour
    {
        public Image[] Cells;

        private void Awake()
        {
            WeaponAddInInventory();
        }

        private void WeaponAddInInventory()
        {
            for (var i = 0; i < WeaponManager.Instance.Weapons.Count; i++)
            {
                Cells[i].sprite = Resources.Load<Sprite>(WeaponManager.Instance.Weapons[i].SpriteUi);
            }
        }

        private void OnEnable()
        {
            WeaponAddInInventory();

            WeaponEvents.WeaponAddInInventory += WeaponAddInInventory;
        }

        private void OnDisable()
        {
            WeaponEvents.WeaponAddInInventory -= WeaponAddInInventory;
        }
    }
}

