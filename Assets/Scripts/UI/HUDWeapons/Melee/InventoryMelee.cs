using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class InventoryMelee : MonoBehaviour
    {
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private GameObject[] _cell;

        private void WeaponMeleeAddInInventory()
        {
            var weaponManager = WeaponManager.Instance.MeleeWeapons;
            _cell[weaponManager.Count - 1].SetActive(true);
            _cell[weaponManager.Count - 1].GetComponent<Image>().sprite = weaponManager[weaponManager.Count - 1].SpriteUi;
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
