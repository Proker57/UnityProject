using UnityEngine;

namespace BOYAREngine
{
    public class SwordBroken : MonoBehaviour, ISaveable
    {
        public bool IsActive;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.name.Equals("Low Collider")) return;

            WeaponEvents.WeaponPickUp(new WeaponSwordBroken());

            IsActive = false;

            _spriteRenderer.enabled = IsActive;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = IsActive;
        }


        public object CaptureState()
        {
            return new SwordBrokenData
            {
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SwordBrokenData)state;

            IsActive = saveData.IsActive;

            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class SwordBrokenData
    {
        public bool IsActive;
    }
}