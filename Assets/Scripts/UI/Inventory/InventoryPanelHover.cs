using System;
using System.Collections;
using System.Globalization;
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
        [SerializeField] private Text _weaponDamage;
        [SerializeField] private Text _weaponAttackSpeed;
        [SerializeField] private Text _weaponDescription;

        [Header("Values")]

        public Text WeaponType;
        public Text WeaponName;
        public Text WeaponDamage;
        public Text WeaponAttackSpeed;
        public Text WeaponDescription;
        public Text WeaponPrice;

        // TODO Add description text to the hover panel
        public Text ItemType;
        public Text ItemName;
        public Text ItemDescriptionValue;
        public Text ItemPrice;

        private void Start()
        {
            LoadStrings();
        }

        public void ShowPanel()
        {
            // TODO Change input scheme
            WeaponManager.Instance.IsAbleToAttack = false;

            var index = int.Parse(name);

            switch (Inventory.Instance.CurrentTab)
            {
                case Inventory.TabType.Weapons:
                    var weaponManager = WeaponManager.Instance;
                    if (weaponManager.Weapons.Count <= index) return;
                    WeaponType.text = weaponManager.Weapons[index].Type;
                    WeaponName.text = "<color=#9AEE49>" + weaponManager.Weapons[index].Name + "</color>";
                    WeaponDamage.text = weaponManager.Weapons[index].Damage.ToString();
                    WeaponAttackSpeed.text = weaponManager.Weapons[index].AttackSpeed.ToString(CultureInfo.InvariantCulture);
                    WeaponDescription.text = weaponManager.Weapons[index].Description;
                    WeaponPrice.text = weaponManager.Weapons[index].SellCost + "G";

                    _hoverWeaponsPanel.SetActive(true);
                    break;
                case Inventory.TabType.Items:
                    var itemManager = ItemManager.Instance;
                    if (itemManager.Items.Count <= index) return;
                    ItemType.text = itemManager.Items[index].Type;
                    ItemName.text = "<color=#9AEE49>" + itemManager.Items[index].Name + "</color>";
                    ItemDescriptionValue.text = itemManager.Items[index].Description;
                    ItemPrice.text = itemManager.Items[index].SellCost + "G";

                    _hoverItemsPanel.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClosePanel()
        {
            WeaponManager.Instance.IsAbleToAttack = true;

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
            LoadStrings();
        }

        private async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                _weaponName.text = GetLocalizedString(stringTable, "name");
                _weaponDamage.text = GetLocalizedString(stringTable, "damage");
                _weaponAttackSpeed.text = GetLocalizedString(stringTable, "attack_speed");
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
