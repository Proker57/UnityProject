using UnityEngine;

namespace BOYAREngine
{
    public class NextItemButton : MonoBehaviour
    {
        public void NextItem()
        {
            ItemEvents.ItemNext();
        }
    }
}
