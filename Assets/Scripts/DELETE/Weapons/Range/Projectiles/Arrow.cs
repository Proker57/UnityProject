using System.Collections;
using UnityEngine;

namespace BOYAREngine
{
    public class Arrow : MonoBehaviour
    {
#pragma warning disable 649

        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private float _speed;
        [SerializeField] private int _liveTime;

#pragma warning restore 649

        private void Start()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PlayerProjectile"));

            StartCoroutine(LiveTime(_liveTime));
        }

        private IEnumerator LiveTime(int liveTime)
        {
            while (liveTime > 0)
            {
                yield return new WaitForSeconds(1);
                liveTime--;
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (((1 << collider.gameObject.layer) & _damageableLayer) != 0)
            {
                collider.GetComponent<IDamageable>().GetDamage(Bow.Damage);
                Destroy(gameObject);
            }
        }
    }
}
