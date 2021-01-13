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

        private void Awake()
        {
            Reset = 0;
            NextAttackCheck = 1.4f;
        }

        internal virtual void SetAnimations()
        {
            animations = new List<string>();
        }

        internal void PrimaryAttack() { }

        internal virtual void FirstAttack() { }

        internal virtual void SecondAttack() { }

        internal virtual void ThirdAttack() { }

        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
