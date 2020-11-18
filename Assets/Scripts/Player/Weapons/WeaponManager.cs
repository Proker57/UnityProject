using UnityEngine;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour
    {
        public static int CurrentWeapon { get; set; }
        public int Damage;

        private Sword _sword;
        private Bow _bow;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _sword = new Sword();
            _bow = new Bow();

            SetWeapon((int) WeaponEnum.Weapon.None);
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;

            switch (CurrentWeapon)
            {
                case (int)WeaponEnum.Weapon.None:
                    Damage = 0;
                    break;
                case (int) WeaponEnum.Weapon.Sword:
                    Damage = _sword.Damage;
                    break;
                case (int)WeaponEnum.Weapon.Bow:
                    Damage = Bow.Damage;
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
    }
}
