using UnityEngine;

namespace BOYAREngine
{
    public class ItemEvents
    {
        public delegate void ItemPickUpDelegate(int itemId);
        public static ItemPickUpDelegate ItemPickUp;

        public delegate void ItemNextDelegate();
        public static ItemNextDelegate ItemNext;
    }
}