using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ItemSlotRight : MonoBehaviour
    {
        private const string StringTableCollectionName = "Item_names";

        [HideInInspector] public Player Player;

        [SerializeField] private UIManagerLegacy _uiManagerLegacy;
        [SerializeField] private ItemSprites _itemSprite;
        [SerializeField] private Text _itemName;
        private Image _itemImage;

        private string _none;
        private string _potion_hp_small;
        private string _potion_hp_medium;
        private string _potion_hp_huge;

        private void Awake()
        {
            StartCoroutine(LoadStrings());
            _itemSprite = GetComponent<ItemSprites>();
            _itemImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (Player != null)
            {
                ShowItemUi();
            }
        }

        private void ShowItemUi()
        {
            if (Player.ItemManager.ItemIndex >= 0)
            {
                foreach (var t in Player.ItemManager.Items)
                {
                    _itemImage.sprite = Player.ItemManager.Items[Player.ItemManager.ItemIndex].Sprite;
                    _itemName.text = Player.ItemManager.Items[Player.ItemManager.ItemIndex].Name;
                }
            }
            else
            {
                _itemImage.sprite = ItemSprites.Instance.None;
                _itemName.text = _none;
            }
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
            Events.PlayerOnScene -= AssignPlayer;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            StartCoroutine(LoadStrings());
        }

        private IEnumerator LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                _none = GetLocalizedString(stringTable, "none");
                _potion_hp_small = GetLocalizedString(stringTable, "potion_hp_small");
                _potion_hp_medium = GetLocalizedString(stringTable, "potion_hp_medium");
                _potion_hp_huge = GetLocalizedString(stringTable, "potion_hp_huge");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
            }
        }

        private static string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
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
