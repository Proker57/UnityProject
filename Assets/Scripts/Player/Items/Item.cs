using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        internal const string StringTableCollectionName = "Item_names";

        [SerializeField] internal Sprite Sprite;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;

        internal virtual void Use() {}
        internal virtual void LoadStrings() {}
    }
}
