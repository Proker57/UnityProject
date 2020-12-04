using UnityEngine;

namespace BOYAREngine
{
    public class ChangeItemButton : MonoBehaviour
    {
        public void PreviousItem()
        {
            ItemEvents.ItemPrevious();
        }

        public void NextItem()
        {
            ItemEvents.ItemNext();
        }
    }
}
