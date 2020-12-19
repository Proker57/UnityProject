using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance = null;

        [SerializeField] private const string EmptySlotSpritePath = "Images/UI/Inventory/Slot_Empty";

        public int ChosenSlot = -1;

        [SerializeField] private GameObject _scrollView;
        [SerializeField] private GameObject[] _cells;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void WeaponMeleeAddInInventory()
        {
            var weaponManager = WeaponManager.Instance.Weapons;

            _cells[weaponManager.Count - 1].GetComponent<Image>().sprite
                = weaponManager[weaponManager.Count - 1].SpriteUi;
        }

        public void Remove()
        {
            WeaponManager.Instance.Weapons.RemoveAt(ChosenSlot);

            if (ChosenSlot <= WeaponManager.Instance.CurrentWeapon)
            {
                WeaponManager.Instance.CurrentWeapon--;
            }

            UpdateSprites();
        }

        public void UpdateSprites()
        {
            var i = 0;

            foreach (var cell in _cells)
            {
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
            }
        }

        private void OnEnable()
        {
            WeaponMeleeEvents.WeaponMeleeAddInInventory += WeaponMeleeAddInInventory;
        }

        private void OnDisable()
        {
            WeaponMeleeEvents.WeaponMeleeAddInInventory -= WeaponMeleeAddInInventory;
        }
    }
}
