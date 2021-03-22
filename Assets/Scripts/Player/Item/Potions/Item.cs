using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        internal const string StringTableCollectionName = "Item_names";

        //[SerializeField] internal Sprite SpriteUi;
        //[SerializeField] internal Sprite Sprite;

        internal string Sprite;
        internal string SpriteUi;

        [SerializeField] internal string Type;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;

        [SerializeField] internal int SellCost;

        internal virtual void Use() { }
        internal virtual void LoadStrings() { }
    }
}