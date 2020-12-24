using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ItemHover : MonoBehaviour
    {
        [SerializeField] private GameObject _hoverPanel;
        [SerializeField] private ItemSlotRight _itemSlotRight;
        [SerializeField] private Text _description;

        public void ShowPanel()
        {
            _hoverPanel.SetActive(true);

            if (ItemManager.Instance.CurrentItem >= 0)
            {
                _description.text = ItemManager.Instance.Items[ItemManager.Instance.CurrentItem].Description;
            }
            else
            {
                _description.text = "...";
            }
        }

        public void ClosePanel()
        {
            _hoverPanel.SetActive(false);
        }
    }
}
