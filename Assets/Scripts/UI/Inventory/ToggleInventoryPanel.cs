using UnityEngine;

namespace BOYAREngine
{
    public class ToggleInventoryPanel : MonoBehaviour
    {
        public bool _isActive;

        public GameObject _panel;

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

        private void Inventory_started()
        {
            TogglePanel();
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;

            Inputs.Instance.Input.HUD.Inventory.started -= _ => Inventory_started();
        }

        private void AssignPlayer(bool isActive)
        {
            Inputs.Instance.Input.HUD.Inventory.started += _ => Inventory_started();
        }

    }
}
