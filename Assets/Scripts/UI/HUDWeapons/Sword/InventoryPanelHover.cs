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
    public class InventoryPanelHover : MonoBehaviour
    {
        private const string StringTableCollectionName = "MeleeHoverUI";

        [Header("Inventory")]
        [SerializeField] private GameObject _hoverWeaponsPanel;
        [SerializeField] private GameObject _hoverItemsPanel;
        [SerializeField] private GameObject _inventoryPanel;

        [Header("Text")]
        [SerializeField] private Text _weaponName;
        public Text WeaponNameValue;
        [SerializeField] private Text _weaponDamage;
        public Text WeaponDamageValue;
        [SerializeField] private Text _weaponDescription;
        public Text WeaponDescriptionValue;

        // TODO Add description text to the hover panel
        [SerializeField] private Text _itemDescription;
        public Text ItemDescriptionValue;

        private void Start()
        {
            StartCoroutine(LoadStrings());
        }

        public void ShowPanel()
        {
            // TODO Change input scheme
            Player.Instance.Input.Disable();

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    var weaponManager = WeaponManager.Instance;
                    if (weaponManager.Weapons.Count <= int.Parse(name)) return;
                    WeaponNameValue.text = weaponManager.Weapons[int.Parse(name)].Name;
                    WeaponDamageValue.text = weaponManager.Weapons[int.Parse(name)].Damage.ToString();
                    WeaponDescriptionValue.text = weaponManager.Weapons[int.Parse(name)].Description;
                    _hoverWeaponsPanel.SetActive(true);
                    break;
                case Inventory.TabType.Items:
                    var itemManager = ItemManager.Instance;
                    if (itemManager.Items.Count <= int.Parse(name)) return;
                    ItemDescriptionValue.text = itemManager.Items[int.Parse(name)].Description;
                    _hoverItemsPanel.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClosePanel()
        {
            Player.Instance.Input.Enable();

            _hoverWeaponsPanel.SetActive(false);
            _hoverItemsPanel.SetActive(false);
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
                _weaponName.text = GetLocalizedString(stringTable, "name");
                _weaponDamage.text = GetLocalizedString(stringTable, "damage");
                _weaponDescription.text = GetLocalizedString(stringTable, "description");
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
