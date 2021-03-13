using UnityEngine;

namespace BOYAREngine
{
    public class ToggleHUDPanel : MonoBehaviour
    {
        public bool _isActive;

        public GameObject _panel;

        private Player _player;

        public void TogglePanel()
        {
            _isActive = !_isActive;

            _panel.SetActive(_isActive);
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

        public void EnterPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = true;
        }
    }
}
