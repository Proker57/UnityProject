using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Melee
    {
        internal const string StringTableCollectionName = "Weapon_names";

        [SerializeField] internal Sprite SpriteUi;
        [SerializeField] internal Sprite Sprite;
        [SerializeField] internal string Name;
        [SerializeField] internal int Damage;
        [SerializeField] internal int SellCost;
        [SerializeField] internal float AttackSpeed;
        [SerializeField] internal string Description;

        internal virtual void PrimaryAttack() { }
        internal virtual void SecondaryAttack() { }

        internal virtual void LoadStrings() { }

    }
}
