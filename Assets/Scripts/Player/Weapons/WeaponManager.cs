﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static WeaponManager Instance = null;

        public int Damage;

        public static int CurrentWeapon = -1;
        public bool IsAbleToAttack = true;

        public List<Melee> MeleeWeapons = new List<Melee>();

        public Transform AttackPoint;
        public float Radius;
        public LayerMask DamageLayers;

        private Player _player;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            _player = GetComponent<Player>();
        }

        private void MeleePickUp(Melee weapon)
        {
            MeleeWeapons.Add(weapon);

            WeaponMeleeEvents.WeaponMeleeAddInInventory();
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;
        }

        private void MeleePick_started()
        {
            Debug.Log("Melee pick");
        }

        private void RangePick_started()
        {
            Debug.Log("Range pick");
        }

        private void PrimaryAttack_started()
        {
            if (MeleeWeapons.Count > 0 && CurrentWeapon >= 0)
            {
                MeleeWeapons[CurrentWeapon].PrimaryAttack();
            }
        }

        private void SecondaryAttack_started()
        {
            if (MeleeWeapons.Count > 0 && CurrentWeapon >= 0)
            {
                MeleeWeapons[CurrentWeapon].SecondaryAttack();
            }
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.MeleePick.started += _ => MeleePick_started();
            _player.Input.PlayerInGame.RangePick.started += _ => RangePick_started();

            _player.Input.PlayerInGame.PrimaryAttack.started += _ => PrimaryAttack_started();
            _player.Input.PlayerInGame.SecondaryAttack.started += _ => SecondaryAttack_started();

            WeaponMeleeEvents.WeaponMeleePickUp += MeleePickUp;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.MeleePick.started -= _ => MeleePick_started();
            _player.Input.PlayerInGame.RangePick.started -= _ => RangePick_started();

            _player.Input.PlayerInGame.PrimaryAttack.started -= _ => PrimaryAttack_started();
            _player.Input.PlayerInGame.SecondaryAttack.started -= _ => SecondaryAttack_started();

            WeaponMeleeEvents.WeaponMeleePickUp -= MeleePickUp;

            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            foreach (var weapon in MeleeWeapons)
            {
                weapon.LoadStrings();
            }
        }

        public object CaptureState()
        {
            return new WeaponManagerData()
            {
                CurrentWeapon = CurrentWeapon,
                Damage = Damage,
                IsAbleToAttack = IsAbleToAttack,
                MeleeWeapons = MeleeWeapons
            };
        }

        public void RestoreState(object state)
        {
            var weaponManagerData = (WeaponManagerData) state;
            CurrentWeapon = weaponManagerData.CurrentWeapon;
            Damage = weaponManagerData.Damage;
            IsAbleToAttack = weaponManagerData.IsAbleToAttack;
            MeleeWeapons = weaponManagerData.MeleeWeapons;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null) return;

            Gizmos.DrawWireSphere(AttackPoint.transform.position, Radius);
        }
#endif
    }

    [System.Serializable]
    public class WeaponManagerData
    {
        public int CurrentWeapon;
        public int Damage;
        public bool IsAbleToAttack;

        public List<Melee> MeleeWeapons;
    }
}
