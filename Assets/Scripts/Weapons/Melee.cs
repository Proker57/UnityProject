using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Melee
    {
        internal const string StringTableCollectionName = "Weapon_names";

        internal string Sprite;
        internal string SpriteUi;

        [SerializeField] internal string Type;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;

        [SerializeField] internal int Damage;
        [SerializeField] internal int SellCost;

        [SerializeField] internal float AttackSpeed;
        [SerializeField] protected float AttackSpeedBase;
        [SerializeField] internal float Radius;

        [SerializeField] internal float PushForce;

        public void Reset()
        {
            AttackSpeed = AttackSpeedBase;
        }

        internal virtual int FirstAttack() { return Damage; }

        internal virtual int SecondAttack() { return Damage; }

        internal virtual int ThirdAttack() { return Damage; }

        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
