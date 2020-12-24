using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        internal const string StringTableCollectionName = "Item_names";

        internal enum ItemType
        {
            Potion,
        };

        [SerializeField] internal Sprite SpriteUi;
        [SerializeField] internal Sprite Sprite;

        [SerializeField] internal ItemType Type;

        [SerializeField] internal string Name;
        [SerializeField] internal string Description;

        [SerializeField] internal int SellCost;

        internal virtual void Use() { }
        internal virtual void LoadStrings() { }
    }
}