namespace BOYAREngine
{
    public class WeaponMeleeEvents
    {
        public delegate void WeaponPickUpDelegate(Melee meleeWeapon);
        public static WeaponPickUpDelegate WeaponMeleePickUp;

        public delegate void WeaponAddInInventoryDelegate();
        public static WeaponAddInInventoryDelegate WeaponMeleeAddInInventory;
    }
}
