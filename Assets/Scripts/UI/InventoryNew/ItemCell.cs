using UnityEngine;
using UnityEngine.EventSystems;

namespace BOYAREngine.UI
{
    public class ItemCell : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private const string EmptySlotSpritePath = "Images/UI/Inventory/Slot_Empty";

        [SerializeField] private ItemsTab _itemsTab;
        private ItemsHover _itemHover;

        [HideInInspector] public int CellIndex;

        private void Awake()
        {
            _itemHover = GetComponent<ItemsHover>();
            CellIndex = int.Parse(name);
        }

        public void OnClick()
        {
            if (ItemManager.Instance.Items.Count > CellIndex)
            {
                ItemManager.Instance.Items[CellIndex].Use();

                _itemsTab.Cells[CellIndex].sprite =
                    Resources.Load<Sprite>(EmptySlotSpritePath);

                _itemsTab.Cells[CellIndex].sprite = Resources.Load<Sprite>(EmptySlotSpritePath);

                ItemManager.Instance.Items.RemoveAt(CellIndex);

            }
        }

        public void HoverPanel(bool isOn)
        {
            if (ItemManager.Instance.Items.Count > CellIndex)
            {
                _itemHover.ShowPanel(isOn);
            }

        }

        public void OnSelect(BaseEventData eventData)
        {
            if (ItemManager.Instance.Items.Count > CellIndex)
            {
                _itemHover.ShowPanel(true);
            }

        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (ItemManager.Instance.Items.Count > CellIndex)
            {
                _itemHover.ShowPanel(false);
            }
        }
    }
}