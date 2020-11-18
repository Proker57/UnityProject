using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class UIManagerLegacy : MonoBehaviour
    {
        public GameObject GameController;
        public GameObject LevelUpGroup;
        [Space]
        private SceneLoader _sceneLoader;
        private Canvas _canvas;
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _DoubleJumpUI;
        [SerializeField] private UIInput _uInputActionAsset;

        private void Awake()
        {
            _sceneLoader = GameController.GetComponent<SceneLoader>();

            _canvas = transform.GetChild(0).GetComponent<Canvas>();

            _uInputActionAsset = new UIInput();
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

        private void LevelUp()
        {
            LevelUpGroup.SetActive(true);
            Debug.Log("Level UP");
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive += CheckDashIsActive;
            HUDEvents.JumpCheckIsActive += CheckJumpIsActive;
            _uInputActionAsset.Enable();

            PlayerEvents.LevelUp += LevelUp;
        }

        private void OnDestroy()
        {
            HUDEvents.DashCheckIsActive -= CheckDashIsActive;
            HUDEvents.JumpCheckIsActive -= CheckJumpIsActive;
            _uInputActionAsset.Disable();

            PlayerEvents.LevelUp -= LevelUp;
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
