using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine.UI
{
    public class ItemsTab : MonoBehaviour
    {
        public Image[] Cells;

        private void Awake()
        {
            //ItemAddInInventory();
        }

        private void ItemAddInInventory()
        {
            for (var i = 0; i < ItemManager.Instance.Items.Count; i++)
            {
                Cells[i].sprite = Resources.Load<Sprite>(ItemManager.Instance.Items[i].SpriteUi);
            }
        }

        private void OnEnable()
        {
            ItemAddInInventory();

            ItemEvents.ItemAddInInventory += ItemAddInInventory;
        }

        private void OnDisable()
        {
            ItemEvents.ItemAddInInventory -= ItemAddInInventory;
        }
    }
}