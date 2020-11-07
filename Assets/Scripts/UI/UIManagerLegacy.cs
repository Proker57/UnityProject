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
        private Canvas _canvas;
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

            _canvas = transform.GetChild(0).GetComponent<Canvas>();
        }

        private void Update()
        {
            if (_sceneLoader.CurrentSceneName.Equals("Main") || _sceneLoader.CurrentSceneName.Equals("MainMenu"))
            {
                _canvas.enabled = false;
            }
            else
            {
                _canvas.enabled = true;
            }
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive += CheckDashIsActive;
            HUDEvents.JumpCheckIsActive += CheckJumpIsActive;
        }

        private void OnDestroy()
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
