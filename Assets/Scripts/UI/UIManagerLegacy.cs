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

        [SerializeField] private Image _itemImage;
        [SerializeField] private ItemImages _itemSprite;
        [SerializeField] private Text _itemName;
        private Player _player;

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

            if (_player != null)
            {
                if (_player.Stats.LevelUpPoints != 0)
                {
                    LevelUpPoints.gameObject.SetActive(true);
                    LevelUpPoints.text = "LP:" + _player.Stats.LevelUpPoints;
                }
                else
                {
                    LevelUpPoints.gameObject.SetActive(false);
                }

                ShowItemUI();
            }
        }

        private void ShowItemUI()
        {
            switch (_player.ItemManager.ItemIndex)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    _itemImage.sprite = _itemSprite.SmallPotion;
                    _itemName.text = "Small Potion";
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    _itemImage.sprite = _itemSprite.MediumPotion;
                    _itemName.text = "Medium Potion";
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    _itemImage.sprite = _itemSprite.HugePotion;
                    _itemName.text = "Huge Potion";
                    break;
                default:
                    _itemImage.sprite = _itemSprite.None;
                    _itemName.text = "No items";
                    break;
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
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
        }
    }
}
