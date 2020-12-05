using UnityEngine;

namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        public readonly int ItemId;

        public string Name;
        public Sprite Sprite;

        public Item(int itemId)
        {
            ItemId = itemId;

            switch (ItemId)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    Sprite = ItemSprites.Instance.SmallPotion;
                    Name = "Small Potion";
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    Sprite = ItemSprites.Instance.MediumPotion;
                    Name = "Medium Potion";
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    Sprite = ItemSprites.Instance.HugePotion;
                    Name = "Huge Potion";
                    break;
                default:
                    Sprite = ItemSprites.Instance.None;
                    Name = "None";
                    break;
            }
        }

        public void UseItem()
        {
            switch (ItemId)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    PlayerEvents.RestoreHealth(20);
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    PlayerEvents.RestoreHealth(50);
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    PlayerEvents.RestoreHealth(100);
                    break;
            }
        }
    }
}
