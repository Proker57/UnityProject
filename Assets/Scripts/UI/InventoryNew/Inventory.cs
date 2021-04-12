using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace BOYAREngine.UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [SerializeField] private GameObject _panel;

        [SerializeField] private GameObject _selectedObject;

        [Space]
        [SerializeField] private InputActionAsset _controls;

        private void Start()
        {
            _controls.FindActionMap("PlayerInGame").FindAction("Inventory").started += Inventory_pressed;
            _controls.FindActionMap("Inventory").FindAction("Close").started += Inventory_pressed;
        }

        private void Inventory_pressed(InputAction.CallbackContext obj)
        {
            OpenInventory();
        }

        public void OpenInventory()
        {
            _panel.SetActive(!_panel.activeSelf);

            EventSystem.current.SetSelectedGameObject(_selectedObject);

            InputToggles.Inventory(_panel.activeSelf);
        }
    }
}
