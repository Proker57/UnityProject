using UnityEngine;

namespace BOYAREngine.UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [SerializeField] private GameObject _panel;

        public void OpenInventory()
        {
            _panel.SetActive(!_panel.activeSelf);
        }
    }
}
