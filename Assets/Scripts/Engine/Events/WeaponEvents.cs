namespace BOYAREngine
{
    public class WeaponEvents
    {
        public delegate void WeaponPickUpDelegate(Melee weapon);
        public static WeaponPickUpDelegate WeaponPickUp;

        public delegate void WeaponAddInInventoryDelegate();
        public static WeaponAddInInventoryDelegate WeaponAddInInventory;
    }
}
