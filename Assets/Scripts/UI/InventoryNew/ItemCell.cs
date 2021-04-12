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

                ItemManager.Instance.Items.RemoveAt(CellIndex);

                UpdateSprites();
            }
        }

        private void UpdateSprites()
        {
            for (var i = 0; i < ItemManager.Instance.Items.Count; i++)
            {
                _itemsTab.Cells[i].sprite = Resources.Load<Sprite>(ItemManager.Instance.Items[i].SpriteUi);
            }

            _itemsTab.Cells[ItemManager.Instance.Items.Count].sprite = Resources.Load<Sprite>(EmptySlotSpritePath);
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