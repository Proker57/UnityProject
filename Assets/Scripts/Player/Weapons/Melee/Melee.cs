using UnityEngine;
using UnityEngine.Localization.Tables;

namespace BOYAREngine
{
    [System.Serializable]
    public class Melee
    {
        internal const string StringTableCollectionName = "Weapon_names";

        internal enum WeaponType 
        {
            Sword,
            Bow
        };

        [SerializeField] internal Sprite SpriteUi;
        [SerializeField] internal Sprite Sprite;
        [SerializeField] internal string Name;
        [SerializeField] internal WeaponType Type;
        [SerializeField] internal int Damage;
        [SerializeField] internal int SellCost;
        [SerializeField] internal float AttackSpeed;
        [SerializeField] internal float Radius;
        [SerializeField] internal string Description;

        internal float NextAttackCheck;

        internal virtual void PrimaryAttack()
        {
            if (!WeaponManager.Instance.IsAbleToAttack) return;
            if (!(Time.time >= NextAttackCheck)) return;
            var hit = Physics2D.OverlapCircleAll(
                WeaponManager.Instance.AttackPoint.position,
                Radius,
                WeaponManager.Instance.DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<Damageable>().GetDamage(Damage);
            }

            NextAttackCheck = Time.time + AttackSpeed;

            Player.Instance.Animator.SetTrigger("PrimaryAttackSword");
            Debug.Log("Primary attack");
        }

        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
