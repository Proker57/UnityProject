using System.Collections;
using UnityEngine;

namespace BOYAREngine
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _delay;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            StartCoroutine("DealDamage");
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            StopCoroutine("DealDamage");
        }

        private IEnumerator DealDamage()
        {
            while (true)
            {
                PlayerEvents.Damage(_damage);

                yield return new WaitForSeconds(_delay);
            }
        }
    }
}
