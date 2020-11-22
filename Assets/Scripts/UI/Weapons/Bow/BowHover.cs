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
        private string StringTableCollectionName = "BowHoverUI";

        [SerializeField] private GameObject _hoverPanel;
        [SerializeField] private Text _name;
        public Text NameValue;
        [SerializeField] private Text _level;
        public Text LevelValue;
        [SerializeField] private Text _damage;
        public Text DamageValue;

        private string _localizedName;

        private void Awake()
        {
            StartCoroutine(LoadStrings());
        }

        public void ShowPanel()
        {
            _hoverPanel.SetActive(true);

            NameValue.text = _localizedName;
            LevelValue.text = Bow.Level.ToString();
            DamageValue.text = Bow.Damage.ToString();
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
                _level.text = GetLocalizedString(stringTable, "level");
                _damage.text = GetLocalizedString(stringTable, "damage");

                _localizedName = GetLocalizedString(stringTable, "weapon");
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
