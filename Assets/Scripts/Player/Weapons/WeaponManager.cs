using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static WeaponManager Instance;

        public int CurrentWeapon = -1;
        public bool IsAbleToAttack = true;

        public List<Melee> Weapons = new List<Melee>();

        public Animator Animator;
        public Transform AttackPoint;
        public LayerMask DamageLayers;

        [SerializeField] private SpriteRenderer _weaponSprite = null;

        private Player _player;

        [SerializeField] private InputAction _primaryAttack;
        [SerializeField] private InputActionAsset _controls;

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

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _primaryAttack = iam.FindAction("PrimaryAttack");
            _primaryAttack.performed += PrimaryAttack_started;
        }

        private void Update()
        {
            if (Weapons == null || CurrentWeapon == -1) return;
            Weapons[CurrentWeapon].Reset += Time.deltaTime;
            if (Weapons[CurrentWeapon].CurrentComboNumber <= 0) return;
            if (!(Weapons[CurrentWeapon].Reset > Weapons[CurrentWeapon].NextAttackCheck)) return;
            Animator.SetTrigger("Reset");
            Weapons[CurrentWeapon].CurrentComboNumber = 0;
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

            _weaponSprite.sprite = Resources.Load<Sprite>(Weapons[CurrentWeapon].Sprite);
        }

        private void PrimaryAttack_started(InputAction.CallbackContext ctx)
        {
            if (!IsAbleToAttack) return;
            if (Weapons.Count <= 0 || CurrentWeapon < 0) return;
            if (Weapons[CurrentWeapon].CurrentComboNumber < Weapons[CurrentWeapon].MaxComboNumber)
            {
                Animator.SetTrigger("PrimaryAttackSword");
                Weapons[CurrentWeapon].CurrentComboNumber++;
                Weapons[CurrentWeapon].Reset = 0f;
            }

            if (Weapons[CurrentWeapon].CurrentComboNumber <= 0) return;
            if (Weapons[CurrentWeapon].CurrentComboNumber == Weapons[CurrentWeapon].MaxComboNumber)
            {
                AttackOverlap(Weapons[CurrentWeapon].ThirdAttack());

                Weapons[CurrentWeapon].NextAttackCheck = 3f;
                Weapons[CurrentWeapon].CurrentComboNumber = 0;
            }
            else
            {
                Weapons[CurrentWeapon].NextAttackCheck = 1f;
            }

            switch (Weapons[CurrentWeapon].CurrentComboNumber)
            {
                case 1:
                    AttackOverlap(Weapons[CurrentWeapon].FirstAttack());
                    return;
                case 2:
                    AttackOverlap(Weapons[CurrentWeapon].SecondAttack());
                    return;
            }
        }

        private void AttackOverlap(int damage)
        {
            var hit = Physics2D.OverlapCircleAll(AttackPoint.transform.position, Weapons[CurrentWeapon].Radius, DamageLayers);
            foreach (var enemies in hit)
            {
                try
                {
                    enemies.GetComponent<IDamageable>().GetDamage(damage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void SecondaryAttack_started(InputAction.CallbackContext ctx)
        {
            if (!IsAbleToAttack) return;
            if (Weapons.Count > 0 && CurrentWeapon >= 0)
            {
                Weapons[CurrentWeapon].SecondaryAttack();
            }
        }

        private void OnEnable()
        {
            //_player.Input.PlayerInGame.PrimaryAttack.started += _ => PrimaryAttack_started();
            //_player.Input.PlayerInGame.SecondaryAttack.started += _ => SecondaryAttack_started();

            WeaponEvents.WeaponPickUp += MeleePickUp;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            //_player.Input.PlayerInGame.PrimaryAttack.started -= _ => PrimaryAttack_started();
            //_player.Input.PlayerInGame.SecondaryAttack.started -= _ => SecondaryAttack_started();

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
                IsAbleToAttack = IsAbleToAttack,
                MeleeWeapons = Weapons
            };
        }

        public void RestoreState(object state)
        {
            var weaponManagerData = (WeaponManagerData) state;
            CurrentWeapon = weaponManagerData.CurrentWeapon;
            IsAbleToAttack = weaponManagerData.IsAbleToAttack;
            Weapons = weaponManagerData.MeleeWeapons;

            SetWeapon(CurrentWeapon);
            InventoryOptions.HighlightChosenWeapon();
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);
        }
    }

    [System.Serializable]
    public class WeaponManagerData
    {
        public int CurrentWeapon;
        public bool IsAbleToAttack;

        public List<Melee> MeleeWeapons;
    }
}


