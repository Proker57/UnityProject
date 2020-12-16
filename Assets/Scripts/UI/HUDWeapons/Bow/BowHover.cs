using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class BowHover : MonoBehaviour
    {
        private const string StringTableCollectionName = "RangeHoverUI";

        [SerializeField] private GameObject _hoverPanel;

        [SerializeField] private Text _name;
        public Text NameValue;
        [SerializeField] private Text _damage;
        public Text DamageValue;
        [SerializeField] private Text _description;
        public Text DescriptionValue;

        private void Awake()
        {
            StartCoroutine(LoadStrings());
        }

        public void ShowPanel()
        {
            _hoverPanel.SetActive(true);

            NameValue.text = "Name bow";
            DamageValue.text = Bow.Damage.ToString();
            DescriptionValue.text = Bow.Level.ToString();
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
                _name.text = GetLocalizedString(stringTable, "name");
                _damage.text = GetLocalizedString(stringTable, "damage");
                _description.text = GetLocalizedString(stringTable, "description");
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
    }
}
