namespace BOYAREngine
{
    [System.Serializable]
    public class Item
    {
        public readonly int ItemId;

        public Item(int itemId)
        {
            ItemId = itemId;
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
