using UnityEngine;

namespace BOYAREngine
{
    public class UIManagerLegacy : MonoBehaviour
    {
        private bool isDashActive;
        private static UIManagerLegacy _uiManager = null;

        public GameObject _hud;
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _DoubleJumpUI;

        private void Awake()
        {
            if (_uiManager == null)
            {
                _uiManager = this;
            }
            else if (_uiManager == this)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive += CheckDashIsActive;
            HUDEvents.JumpCheckIsActive += CheckJumpIsActive;
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive -= CheckDashIsActive;
            HUDEvents.JumpCheckIsActive -= CheckJumpIsActive;
        }

        private void CheckDashIsActive(bool dashIsActive)
        {
            _dashUI.SetActive(dashIsActive);
        }

        private void CheckJumpIsActive(bool jumpIsActive)
        {
            _DoubleJumpUI.SetActive(jumpIsActive);
        }
    }
}
