using UnityEngine;

namespace BOYAREngine.UI
{
    public class Cell : MonoBehaviour
    {
        private Inventory _inventory;
        private int _cellIndex;

        private void Awake()
        {
            _inventory = Inventory.Instance;
            _cellIndex = int.Parse(name);
        }

        public void OnClick()
        {
            _inventory.ChosenSlot = _cellIndex;
        }
    }
}

