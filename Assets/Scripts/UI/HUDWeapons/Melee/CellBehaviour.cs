using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class CellBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _sell;
        [SerializeField] private Image _equip;

        [Header("InventoryOptions GameObject")]
        [SerializeField] private InventoryOptionsSprites _sprites;

        private InventoryOptions _cellInventoryOptions;
        private InventoryOptions _equipInventoryOptions;

        private void Awake()
        {
            _cellInventoryOptions = _sell.GetComponent<InventoryOptions>();
            _equipInventoryOptions = _equip.GetComponent<InventoryOptions>();
        }

        public void CellClick()
        {
            InventoryMelee.Instance.ChosenSlot = int.Parse(name);

            _sell.sprite = _sprites.Sell;
            _cellInventoryOptions.IsActive = true;

            _equip.sprite = _sprites.Equip;
            _equipInventoryOptions.IsActive = true;
        }

        private void OnDisable()
        {
            _sell.sprite = _sprites.SellInactive;
            _cellInventoryOptions.IsActive = false;

            _equip.sprite = _sprites.EquipInactive;
            _equipInventoryOptions.IsActive = false;
        }
    }
}
