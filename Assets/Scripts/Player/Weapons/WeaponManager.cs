using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static WeaponManager Instance = null;

        public int CurrentWeapon = -1;
        public bool IsAbleToAttack = true;

        public List<Melee> Weapons = new List<Melee>();

        public Animator Animator;
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
            if (!IsAbleToAttack) return;
            if (Weapons.Count <= 0 || CurrentWeapon < 0) return;
            if (Weapons[CurrentWeapon].CurrentComboNumber < Weapons[CurrentWeapon].MaxComboNumber)
            {
                // TODO Add animation
                //Animator.SetTrigger(animations[CurrentComboNumber]);
                Animator.SetTrigger("PrimaryAttackSword");
                Weapons[CurrentWeapon].CurrentComboNumber++;
                Weapons[CurrentWeapon].Reset = 0f;
            }

            if (Weapons[CurrentWeapon].CurrentComboNumber <= 0) return;
            if (Weapons[CurrentWeapon].CurrentComboNumber == Weapons[CurrentWeapon].MaxComboNumber)
            {
                Weapons[CurrentWeapon].ThirdAttack();

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
                    Weapons[CurrentWeapon].FirstAttack();
                    return;
                case 2:
                    Weapons[CurrentWeapon].SecondAttack();
                    return;
            }
        }

        private void SecondaryAttack_started()
        {
            if (!IsAbleToAttack) return;
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
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.transform.position, Weapons[CurrentWeapon].Radius);
        }
#endif
    }

    [System.Serializable]
    public class WeaponManagerData
    {
        public int CurrentWeapon;
        public bool IsAbleToAttack;

        public List<Melee> MeleeWeapons;
    }
}
