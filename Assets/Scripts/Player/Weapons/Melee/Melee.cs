using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Melee
    {
        internal const string StringTableCollectionName = "Weapon_names";

        [SerializeField] internal List<string> animations;
        [SerializeField] internal Animator Animator;
        [SerializeField] internal Sprite SpriteUi;
        [SerializeField] internal Sprite Sprite;

        [SerializeField] internal string Type;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;

        [SerializeField] internal int Damage;
        [SerializeField] internal int SellCost;
        [SerializeField] internal int CurrentComboNumber;
        [SerializeField] internal int MaxComboNumber;

        [SerializeField] internal float AttackSpeed;
        [SerializeField] internal float Radius;
        
        internal float Reset;
        internal float NextAttackCheck;

        /*internal virtual void PrimaryAttack()
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
        }*/

        private void Awake()
        {
            Reset = 0;
            NextAttackCheck = 1.4f;
        }

        internal virtual void SetAnimations()
        {
            animations = new List<string>();
        }

        internal void PrimaryAttack()
        {
            if (CurrentComboNumber < MaxComboNumber)
            {
                // TODO Add animation
                //Animator.SetTrigger(animations[CurrentComboNumber]);
                Animator.SetTrigger("PrimaryAttackSword");
                CurrentComboNumber++;
                Reset = 0f;
            }

            if (CurrentComboNumber > 0)
            {
                Reset += Time.deltaTime;
                if (Reset > NextAttackCheck)
                {
                    Animator.SetTrigger("Reset");
                    CurrentComboNumber = 0;
                }

                if (CurrentComboNumber == MaxComboNumber)
                {
                    ThirdAttack();

                    NextAttackCheck = 3f;
                    CurrentComboNumber = 0;
                }
                else
                {
                    NextAttackCheck = 1f;
                }

                switch (CurrentComboNumber)
                {
                    case 1:
                        FirstAttack();
                        return;
                    case 2:
                        SecondAttack();
                        return;
                }
            }
        }

        internal virtual void FirstAttack()
        {
            Debug.Log("First attack");
        }

        internal virtual void SecondAttack()
        {
            Debug.Log("Second attack");
        }

        internal virtual void ThirdAttack()
        {
            Debug.Log("Third attack");
        }

        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
