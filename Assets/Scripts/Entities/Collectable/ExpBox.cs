using UnityEngine;

namespace BOYAREngine
{
    public class ExpBox : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<BoxCollider2D>().CompareTag("Player"))
            {

            }

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.collider.GetComponent<BoxCollider2D>())
                {
                    Events.GiveExp(20);
                    gameObject.SetActive(false);
                    Debug.Log(contact.collider.name);
                }
            }
        }
    }
}
