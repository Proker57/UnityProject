using UnityEngine;

namespace BOYAREngine
{
    public class Arrow : MonoBehaviour
    {
#pragma warning disable 649

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _speed;

#pragma warning restore 649

        private void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.IsTouchingLayers(_layerMask))
            {
                
                Debug.Log(collision.collider.GetComponent<Damageable>());
                Destroy(gameObject);
            }
        }
    }
}
