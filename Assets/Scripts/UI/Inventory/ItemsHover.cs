using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine.UI
{
    public class ItemsHover : MonoBehaviour
    {
        //private const string StringTableCollectionName = "InventoryHoverPanel";

        [SerializeField] private GameObject _hoverPanel;
        private ItemCell _itemCell;

        [Header("Values")]

        public Text ItemType;
        public Text ItemName;
        public Text ItemDescriptionValue;
        public Text ItemPrice;

        private void Awake()
        {
            _itemCell = GetComponent<ItemCell>();

            /*LoadStrings()*/;
        }

        public void ShowPanel(bool isOn)
        {
            Attack.Instance.IsAbleToAttack = !isOn;

            _hoverPanel.SetActive(isOn);

            var itemManager = ItemManager.Instance;
            if (itemManager.Items.Count <= _itemCell.CellIndex) return;
            ItemType.text = itemManager.Items[_itemCell.CellIndex].Type;
            ItemName.text = "<color=#9AEE49>" + itemManager.Items[_itemCell.CellIndex].Name + "</color>";
            ItemDescriptionValue.text = itemManager.Items[_itemCell.CellIndex].Description;
            ItemPrice.text = itemManager.Items[_itemCell.CellIndex].SellCost + "G";
        }

        /*private void OnEnable()
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
        }*/
    }
}

