using UnityEngine;

namespace BOYAREngine
{
    public class InventoryCloseActions : MonoBehaviour
    {
        public GameObject HoverWeaponPanel;

        private void OnDisable()
        {
            HoverWeaponPanel.SetActive(false);
        }
    }
}