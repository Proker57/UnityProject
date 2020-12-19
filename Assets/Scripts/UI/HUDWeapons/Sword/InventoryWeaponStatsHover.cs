using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class InventoryWeaponStatsHover : MonoBehaviour
    {
        private const string StringTableCollectionName = "MeleeHoverUI";

        [Header("Inventory")]
        [SerializeField] private GameObject _hoverPanel;
        [SerializeField] private GameObject _inventoryPanel;

        [Header("Text")]
        [SerializeField] private Text _name;
        public Text NameValue;
        [SerializeField] private Text _damage;
        public Text DamageValue;
        [SerializeField] private Text _description;
        public Text DescriptionValue;

        private void Start()
        {
            StartCoroutine(LoadStrings());
        }

        public void ShowPanel()
        {
            var weaponManager = WeaponManager.Instance;
            // TODO Change input scheme
            Player.Instance.Input.Disable();

            if (weaponManager.Weapons.Count <= int.Parse(name)) return;
            NameValue.text = weaponManager.Weapons[int.Parse(name)].Name;
            DamageValue.text = weaponManager.Weapons[int.Parse(name)].Damage.ToString();
            DescriptionValue.text = weaponManager.Weapons[int.Parse(name)].Description;
            _hoverPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            Player.Instance.Input.Enable();

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
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }

        private static string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
