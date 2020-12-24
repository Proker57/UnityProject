namespace BOYAREngine
{
    public class ItemEvents
    {
        public delegate void ItemPickUpDelegate(Item item);
        public static ItemPickUpDelegate ItemPickUp;

        public delegate void ItemAddInInventoryDelegate();
        public static ItemAddInInventoryDelegate ItemAddInInventory;

        public delegate void ItemNextDelegate();
        public static ItemNextDelegate ItemNext;

        public delegate void ItemPreviousDelegate();
        public static ItemPreviousDelegate ItemPrevious;
    }
}