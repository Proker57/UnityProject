using System.Globalization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine.UI
{
    public class WeaponsHover : MonoBehaviour
    {
        private const string StringTableCollectionName = "InventoryHoverPanel";

        [SerializeField] private GameObject _hoverPanel;
        private WeaponCell _weaponCell;

        [Header("Text")]
        [SerializeField] private Text _weaponDamage;
        [SerializeField] private Text _weaponAttackSpeed;
        [SerializeField] private Text _weaponDescription;

        [Header("Values")]

        public Text Type;
        public Text Name;
        public Text Damage;
        public Text AttackSpeed;
        public Text Description;
        public Text Price;

        private void Awake()
        {
            _weaponCell = GetComponent<WeaponCell>();

            LoadStrings();
        }

        public void ShowPanel(bool isOn)
        {
            Attack.Instance.IsAbleToAttack = !isOn;

            _hoverPanel.SetActive(isOn);

            var weaponManager = WeaponManager.Instance;
            Type.text = weaponManager.Weapons[_weaponCell.CellIndex].Type;
            Name.text = "<color=#9AEE49>" + weaponManager.Weapons[_weaponCell.CellIndex].Name + "</color>";
            Damage.text = weaponManager.Weapons[_weaponCell.CellIndex].Damage.ToString();
            AttackSpeed.text = weaponManager.Weapons[_weaponCell.CellIndex].AttackSpeed.ToString(CultureInfo.InvariantCulture);
            Description.text = weaponManager.Weapons[_weaponCell.CellIndex].Description;
            Price.text = weaponManager.Weapons[_weaponCell.CellIndex].SellCost + "G";
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;

            LoadStrings();
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

                var splitString = GetLocalizedString(stringTable, "weapons").Split('\n');

                _weaponDamage.text = splitString[0];
                _weaponAttackSpeed.text = splitString[1];
                _weaponDescription.text = splitString[2];
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

