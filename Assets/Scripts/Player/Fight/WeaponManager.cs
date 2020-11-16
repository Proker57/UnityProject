using UnityEngine;

namespace BOYAREngine
{
    public class WeaponManager : MonoBehaviour
    {
        public int CurrentWeapon { get; set; }
        public int Damage;

        private Sword _sword;

        private void Start()
        {
            _sword = new Sword();

            SetWeapon((int) WeaponEnum.Weapon.Sword);
        }

        public void SetWeapon(int weaponIndex)
        {
            CurrentWeapon = weaponIndex;

            if (CurrentWeapon.Equals((int) WeaponEnum.Weapon.Sword))
            {
                Damage = _sword.Damage;
            }
        }
    }
}
