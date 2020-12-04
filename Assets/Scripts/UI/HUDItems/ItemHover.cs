using System.Collections;
using BOYAREngine;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MyNamespace
{
    public class ItemHover : MonoBehaviour
    {
        private const string StringTableCollectionName = "ItemHoverUI";

        [SerializeField] private GameObject _hoverPanel;
        [SerializeField] private ItemSlotRight _itemSlotRight;
        [SerializeField] private Text _description;

        private string _small_potion;
        private string _medium_potion;
        private string _huge_potion;

        private void Awake()
        {
            StartCoroutine(LoadStrings());
        }

        public void ShowPanel()
        {
            _hoverPanel.SetActive(true);

            switch (_itemSlotRight.Player.ItemManager.ItemIndex)
            {
                case (int)ItemEnum.ItemType.SmallPotion:
                    _description.text = _small_potion;
                    break;
                case (int)ItemEnum.ItemType.MediumPotion:
                    _description.text = _medium_potion;
                    break;
                case (int)ItemEnum.ItemType.HugePotion:
                    _description.text = _huge_potion;
                    break;
                default:
                    _description.text = "...";
                    break;
            }
        }

        public void ClosePanel()
        {
            _hoverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
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

                _small_potion = GetLocalizedString(stringTable, "small_potion");
                _medium_potion = GetLocalizedString(stringTable, "medium_potion");
                _huge_potion = GetLocalizedString(stringTable, "huge_potion");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }

        private string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
