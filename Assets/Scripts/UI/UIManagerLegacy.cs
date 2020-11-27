using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class UIManagerLegacy : MonoBehaviour
    {
        public GameObject GameController;
        public GameObject LevelUpGroup;
        public Text LevelUpPoints;
        [Space]
        private SceneLoader _sceneLoader;
        private Canvas _canvas;
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _DoubleJumpUI;

        [HideInInspector] public Player Player;

        private void Awake()
        {
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

            if (Player != null)
            {
                if (Player.Stats.LevelUpPoints != 0)
                {
                    LevelUpPoints.gameObject.SetActive(true);
                    LevelUpPoints.text = "LP:" + Player.Stats.LevelUpPoints;
                }
                else
                {
                    LevelUpPoints.gameObject.SetActive(false);
                }
            }
        }

        

        private void LevelUp()
        {
            LevelUpGroup.SetActive(true);
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive += CheckDashIsActive;
            HUDEvents.JumpCheckIsActive += CheckJumpIsActive;

            Events.PlayerOnScene += AssignPlayer;
            PlayerEvents.LevelUp += LevelUp;
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive -= CheckDashIsActive;
            HUDEvents.JumpCheckIsActive -= CheckJumpIsActive;

            Events.PlayerOnScene -= AssignPlayer;
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

        private void AssignPlayer(bool isActive)
        {
            if (Player == null)
            {
                Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
        }
    }
}
