using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static WeaponManager Instance;

        public int CurrentWeapon = -1;
        public List<Melee> Weapons = new List<Melee>();
        [SerializeField] private SpriteRenderer _weaponSprite;


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
        }

        private void WeaponPickUp(Melee weapon)
        {
            Weapons.Add(weapon);

            WeaponEvents.WeaponAddInInventory?.Invoke();
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;
            if (weaponIndex == -1)
            {
                _weaponSprite.sprite = null;
                return;
            }

            Debug.Log(Resources.Load<Sprite>(Weapons[CurrentWeapon].Sprite));
            _weaponSprite.sprite = Resources.Load<Sprite>(Weapons[CurrentWeapon].Sprite);
        }

        private void OnEnable()
        {
            WeaponEvents.WeaponPickUp += WeaponPickUp;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            WeaponEvents.WeaponPickUp -= WeaponPickUp;

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
                MeleeWeapons = Weapons
            };
        }

        public void RestoreState(object state)
        {
            var weaponManagerData = (WeaponManagerData) state;
            CurrentWeapon = weaponManagerData.CurrentWeapon;
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
        public List<Melee> MeleeWeapons;
    }
}


