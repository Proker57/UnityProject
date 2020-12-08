using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace BOYAREngine
{
    public class WoodenPlatform : MonoBehaviour
    {
        private bool _isOnPlatform;

        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            _isOnPlatform = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            _isOnPlatform = false;
        }

        private void JumpOff()
        {
            if (_isOnPlatform)
            {
                _boxCollider2D.enabled = false;
                StartCoroutine("EnableCollider");
            }

            //_boxCollider2D.enabled = false;

            //StartCoroutine("EnableCollider");
        }

        private IEnumerator EnableCollider()
        {
            yield return new WaitForSeconds(1);
            _boxCollider2D.enabled = true;
        }

        private void OnEnable()
        {
            PlayerEvents.JumpDownPlatform += JumpOff;
        }

        private void OnDisable()
        {
            PlayerEvents.JumpDownPlatform -= JumpOff;
        }
    }
}
