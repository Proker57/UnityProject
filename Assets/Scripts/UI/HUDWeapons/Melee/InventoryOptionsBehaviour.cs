using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class InventoryOptionsBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _sell;
        [SerializeField] private Image _equip;

        private const string SellPath = "Images/UI/Inventory/Sell";
        private const string SellInactivePath = "Images/UI/Inventory/Sell_Inactive";
        private const string EquipPath = "Images/UI/Inventory/Equip";
        private const string EquipInactivePath = "Images/UI/Inventory/Equip_Inactive";

        private InventoryOptions _cellInventoryOptions;
        private InventoryOptions _equipInventoryOptions;

        private void Awake()
        {
            _cellInventoryOptions = _sell.GetComponent<InventoryOptions>();
            _equipInventoryOptions = _equip.GetComponent<InventoryOptions>();
        }

        public void CellClick()
        {
            Inventory.Instance.ChosenSlot = int.Parse(name);

            var chosenSlot = Inventory.Instance.ChosenSlot;
            var weaponCount = WeaponManager.Instance.Weapons.Count;

            if (chosenSlot > weaponCount) return;
            _sell.sprite = Resources.Load<Sprite>(SellPath);
            _cellInventoryOptions.IsActive = true;

            _equip.sprite = Resources.Load<Sprite>(EquipPath);
            _equipInventoryOptions.IsActive = true;

        }

        private void OnDisable()
        {
            _sell.sprite = Resources.Load<Sprite>(SellInactivePath);
            _cellInventoryOptions.IsActive = false;

            _equip.sprite = Resources.Load<Sprite>(EquipInactivePath);
            _equipInventoryOptions.IsActive = false;
        }
    }
}
