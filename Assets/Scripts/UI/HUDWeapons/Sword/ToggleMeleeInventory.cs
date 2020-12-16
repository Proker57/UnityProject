using UnityEngine;

namespace BOYAREngine
{
    public class ToggleMeleeInventory : MonoBehaviour
    {
        public bool _isActive;

        public GameObject _inventoryPanel;

        private Player _player;

        public void TogglePanel()
        {
            _isActive = !_isActive;

            _inventoryPanel.SetActive(_isActive);
        }

        private void MeleePick_started()
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

            if (_player != null) _player.Input.PlayerInGame.MeleePick.started -= _ => MeleePick_started();
        }

        private void AssignPlayer(bool isActive)
        {
            _player = Player.Instance;
            _player.Input.PlayerInGame.MeleePick.started += _ => MeleePick_started();
        }
    }
}
