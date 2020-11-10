using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace BOYAREngine
{
    public class ExpBox : MonoBehaviour, ISaveable
    {
        public bool IsActive;
        [SerializeField] private int _expCount;

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

            collision.gameObject.GetComponent<Stats>().GetExp(_expCount);

            IsActive = false;

            _spriteRenderer.enabled = IsActive;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = IsActive;
        }

        public object CaptureState()
        {
            return new ExpBoxData
            {
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (ExpBoxData) state;

            IsActive = saveData.IsActive;

            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class ExpBoxData
    {
        public bool IsActive;
    }
}
