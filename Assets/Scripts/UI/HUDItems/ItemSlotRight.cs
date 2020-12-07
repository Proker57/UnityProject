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
        [SerializeField] private Text _itemName;
        private Image _itemImage;

        private string _none;

        private void Awake()
        {
            StartCoroutine(LoadStrings());

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
            if (ItemManager.Instance.ItemIndex >= 0)
            {
                foreach (var t in ItemManager.Instance.Items)
                {
                    _itemImage.sprite = ItemManager.Instance.Items[ItemManager.Instance.ItemIndex].Sprite;
                    _itemName.text = ItemManager.Instance.Items[ItemManager.Instance.ItemIndex].Name;
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
            Events.PlayerOnScene += AssignPlayer;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;

            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
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
