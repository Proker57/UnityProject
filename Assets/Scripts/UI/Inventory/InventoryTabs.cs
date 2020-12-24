using UnityEngine;

namespace BOYAREngine
{
    public class InventoryTabs : MonoBehaviour
    {
        public void SetWeaponTab()
        {
            Inventory.Instance.CurrentTab = Inventory.TabType.Weapons;
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);
        }

        public void SetItemTab()
        {
            Inventory.Instance.CurrentTab = Inventory.TabType.Items;
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);
        }
    }
}
