using UnityEngine;

namespace BOYAREngine
{
    public class ExpBox : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Player Collider") return;
            Events.GiveExp(20);
            gameObject.SetActive(false);
            Debug.Log(collision.collider.name);
        }
    }
}
