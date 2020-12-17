using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class InventoryMelee : MonoBehaviour
    {
        public static InventoryMelee Instance = null;

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
            var weaponManager = WeaponManager.Instance.MeleeWeapons;
            //_cells[weaponManager.Count - 1].name = (weaponManager.Count - 1).ToString();
            Debug.Log(weaponManager.Count - 1);
            _cells[weaponManager.Count - 1].GetComponent<Image>().sprite = weaponManager[weaponManager.Count - 1].SpriteUi;
            _cells[weaponManager.Count - 1].SetActive(true);
        }

        public void Remove()
        {
            var weaponManager = WeaponManager.Instance.MeleeWeapons;

            _cells[ChosenSlot].GetComponent<Image>().sprite = null;
            _cells[ChosenSlot].SetActive(false);


            if (ChosenSlot < WeaponManager.Instance.CurrentWeapon)
            {
                WeaponManager.Instance.CurrentWeapon--;

                for (var i = ChosenSlot + 1; i < weaponManager.Count; i++)
                {
                    var nameOld = _cells[i].name;
                    _cells[i].name = (int.Parse(nameOld) - 1 + "");

                    if (_cells[i].name == "-1")
                    {
                        _cells[i].name = _cells.Length.ToString();
                    }
                }

                _cells = _cells.OrderBy(x => x.name).ToArray();
            }

            if (WeaponManager.Instance.CurrentWeapon == ChosenSlot)
            {
                WeaponManager.Instance.CurrentWeapon = -1;
            }

            weaponManager.RemoveAt(ChosenSlot);
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
