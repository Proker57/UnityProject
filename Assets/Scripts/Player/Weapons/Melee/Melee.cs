using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Melee
    {
        internal const string StringTableCollectionName = "Weapon_names";

        //[SerializeField] internal Sprite SpriteUi;
        //[SerializeField] internal Sprite Sprite;

        internal string Sprite;
        internal string SpriteUi;

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

        internal void PrimaryAttack() { }

        internal virtual int FirstAttack() { return Damage; }

        internal virtual int SecondAttack() { return Damage; }

        internal virtual int ThirdAttack() { return Damage; }

        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
