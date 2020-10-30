using UnityEngine;

namespace BOYAREngine
{
    public class UIManagerLegacy : MonoBehaviour
    {
        private static UIManagerLegacy _uiManager = null;

        private bool isDashActive;

        public GameObject GameController;

        private SceneLoader _sceneLoader;
        [SerializeField] private GameObject _hud;
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

            _sceneLoader = GameController.GetComponent<SceneLoader>();
        }

        private void Update()
        {
            if (_sceneLoader._currentSceneName.Equals("Main") || _sceneLoader._currentSceneName.Equals("MainMenu"))
            {
                _hud.SetActive(false);
            }
            else
            {
                _hud.SetActive(true);
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
