using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class ToggleInventoryPanel : MonoBehaviour
    {
        public bool _isActive;

        public GameObject _panel;
        [Space]
        [SerializeField] private InputAction _inventory;
        [SerializeField] private InputActionAsset _controls;

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _inventory = iam.FindAction("Inventory");
            _inventory.performed += Inventory_started;
        }

        public void TogglePanel()
        {
            _isActive = !_isActive;

            _panel.SetActive(_isActive);
        }

        public void EnterPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = true;
        }

        private void Inventory_started(InputAction.CallbackContext ctx)
        {
            TogglePanel();
        }
    }
}
