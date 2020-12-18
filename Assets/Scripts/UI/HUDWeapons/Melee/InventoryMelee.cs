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

            _cells[weaponManager.Count - 1].GetComponent<Image>().sprite
                = weaponManager[weaponManager.Count - 1].SpriteUi;
            _cells[weaponManager.Count - 1].SetActive(true);
        }

        public void Remove()
        { 
            _cells[ChosenSlot].GetComponent<Image>().sprite = null;

            if (WeaponManager.Instance.CurrentWeapon == -1)
            {
                Rename(ChosenSlot);

                _cells[ChosenSlot].SetActive(false);

                _cells = _cells.OrderBy(x => x.name).ToArray();

                WeaponManager.Instance.MeleeWeapons.RemoveAt(ChosenSlot);
                return;
            }

            if (ChosenSlot < WeaponManager.Instance.CurrentWeapon)
            {
                WeaponManager.Instance.CurrentWeapon--;

                Rename(ChosenSlot + 1);

                _cells[ChosenSlot].SetActive(false);

                _cells = _cells.OrderBy(x => x.name).ToArray();

                WeaponManager.Instance.MeleeWeapons.RemoveAt(ChosenSlot);
                return;
            }

            if (WeaponManager.Instance.CurrentWeapon == ChosenSlot)
            {
                WeaponManager.Instance.CurrentWeapon = -1;
            }

            WeaponManager.Instance.MeleeWeapons.RemoveAt(ChosenSlot);
            _cells[ChosenSlot].SetActive(false);
        }

        private void Rename(int index)
        {
            for (var i = index; i < _cells.Length; i++)
            {
                var nameOld = _cells[i].name;
                _cells[i].name = (int.Parse(nameOld) - 1 + "");

                if (_cells[i].name == "-1")
                {
                    _cells[i].name = _cells.Length.ToString();
                }
            }

            _cells[ChosenSlot].name = (_cells.Length - 1).ToString();
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
