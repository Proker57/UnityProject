using UnityEngine;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        public static int CurrentWeapon { get; set; }
        public int Damage;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            SetWeapon((int) WeaponEnum.Weapon.None);
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;

            switch (CurrentWeapon)
            {
                case (int) WeaponEnum.Weapon.Sword:
                    Damage = Sword.Damage;
                    break;
                case (int)WeaponEnum.Weapon.Bow:
                    Damage = Bow.Damage;
                    break;
                default:
                    Damage = 0;
                    break;
            }
        }

        private void SwordPick_started()
        {
            SetWeapon((int) WeaponEnum.Weapon.Sword);
        }

        private void BowPick_started()
        {
            SetWeapon((int)WeaponEnum.Weapon.Bow);
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.SwordPick.started += _ => SwordPick_started();
            _player.Input.PlayerInGame.BowPick.started += _ => BowPick_started();
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.SwordPick.started -= _ => SwordPick_started();
            _player.Input.PlayerInGame.BowPick.started -= _ => BowPick_started();
        }

        public object CaptureState()
        {
            return new WeaponManagerData()
            {
                CurrentWeapon = CurrentWeapon,
                Damage = Damage
            };
        }

        public void RestoreState(object state)
        {
            var weaponManagerData = (WeaponManagerData) state;
            CurrentWeapon = weaponManagerData.CurrentWeapon;
            Damage = weaponManagerData.Damage;
        }
    }

    [System.Serializable]
    public class WeaponManagerData
    {
        public int CurrentWeapon;
        public int Damage;
    }
}
