using UnityEngine;

namespace BOYAREngine
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _uiManager = null;

        public GameObject _hud;
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _DoubleJumpUI;
        private Dash _dash;
        private Jump _jump;

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

            SetPlayerComponents();
        }

        private void Update()
        {
            SetPlayerComponents();
            CheckDashIsActive();
            CheckJumpIsActive();
        }

        private void SetPlayerComponents()
        {
            if (FindObjectOfType<Player>() == null) return;
            _dash = FindObjectOfType<Player>().Dash;
            _jump = FindObjectOfType<Player>().Jump;
        }

        private void CheckDashIsActive()
        {
            if (FindObjectOfType<Player>() == null) return;
            _dashUI.SetActive(_dash.enabled);
        }

        private void CheckJumpIsActive()
        {
            if (FindObjectOfType<Player>() == null) return;
            _DoubleJumpUI.SetActive(_jump.enabled);
        }

        private void HUDTurnOn()
        {
            _hud.SetActive(true);
        }

        private void HUDTurnOff()
        {
            _hud.SetActive(false);
        }
    }
}
