using UnityEngine;

namespace BOYAREngine
{
    public class ExpBox : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;
            PlayerEvents.GiveExp(20);
            gameObject.SetActive(false);
        }
    }
}
