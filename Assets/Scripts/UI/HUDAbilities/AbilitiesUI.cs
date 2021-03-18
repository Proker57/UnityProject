using UnityEngine;

namespace BOYAREngine.UI
{
    public class AbilitiesUI : MonoBehaviour
    {
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _doubleJumpUI;

        private Player _player;

        private void CheckDashIsActive(bool dashIsActive)
        {
            _dashUI.SetActive(dashIsActive);
        }

        private void CheckJumpIsActive(bool jumpIsActive)
        {
            _doubleJumpUI.SetActive(jumpIsActive);
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive += CheckDashIsActive;
            HUDEvents.JumpCheckIsActive += CheckJumpIsActive;

            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive -= CheckDashIsActive;
            HUDEvents.JumpCheckIsActive -= CheckJumpIsActive;

            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            if (_player == null) _player = Player.Instance;
        }
    }
}

