using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace BOYAREngine
{
    public class ExpBox : MonoBehaviour, ISaveable
    {
        public bool IsActive;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            PlayerEvents.GiveExp(20);

            IsActive = false;

            _spriteRenderer.enabled = IsActive;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = IsActive;
        }

        public object CaptureState()
        {
            return new SaveData
            {
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData) state;

            IsActive = saveData.IsActive;

            Debug.Log("LOADED BOX");

            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public bool IsActive;
    }
}
