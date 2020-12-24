﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static WeaponManager Instance = null;

        public int Damage;

        public int CurrentWeapon = -1;
        public bool IsAbleToAttack = true;

        public List<Melee> Weapons = new List<Melee>();

        public Transform AttackPoint;
        public LayerMask DamageLayers;

        [SerializeField] private SpriteRenderer _weaponSprite;

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
            Weapons.Add(weapon);

            WeaponEvents.WeaponAddInInventory();
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;
            if (weaponIndex == -1)
            {
                _weaponSprite.sprite = null;
                return;
            }
            _weaponSprite.sprite = Weapons[CurrentWeapon].Sprite;
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
            if (Weapons.Count > 0 && CurrentWeapon >= 0)
            {
                Weapons[CurrentWeapon].PrimaryAttack();
            }
        }

        private void SecondaryAttack_started()
        {
            if (Weapons.Count > 0 && CurrentWeapon >= 0)
            {
                Weapons[CurrentWeapon].SecondaryAttack();
            }
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.MeleePick.started += _ => MeleePick_started();
            _player.Input.PlayerInGame.RangePick.started += _ => RangePick_started();

            _player.Input.PlayerInGame.PrimaryAttack.started += _ => PrimaryAttack_started();
            _player.Input.PlayerInGame.SecondaryAttack.started += _ => SecondaryAttack_started();

            WeaponEvents.WeaponPickUp += MeleePickUp;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.MeleePick.started -= _ => MeleePick_started();
            _player.Input.PlayerInGame.RangePick.started -= _ => RangePick_started();

            _player.Input.PlayerInGame.PrimaryAttack.started -= _ => PrimaryAttack_started();
            _player.Input.PlayerInGame.SecondaryAttack.started -= _ => SecondaryAttack_started();

            WeaponEvents.WeaponPickUp -= MeleePickUp;

            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            foreach (var weapon in Weapons)
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
                MeleeWeapons = Weapons
            };
        }

        public void RestoreState(object state)
        {
            var weaponManagerData = (WeaponManagerData) state;
            CurrentWeapon = weaponManagerData.CurrentWeapon;
            Damage = weaponManagerData.Damage;
            IsAbleToAttack = weaponManagerData.IsAbleToAttack;
            Weapons = weaponManagerData.MeleeWeapons;
        }
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
