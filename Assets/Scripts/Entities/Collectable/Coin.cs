using UnityEngine;

namespace BOYAREngine
{
    public class Coin : MonoBehaviour, ISaveable
    {
#pragma warning disable 649
        [HideInInspector] public bool IsActive;
        public int Amount;
#pragma warning restore 649

        [SerializeField] private GameObject _light;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private Animator _animator;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            //collision.gameObject.GetComponent<Stats>().GetCurrency(Amount);
            PlayerEvents.GiveCurrency?.Invoke(Amount);

            IsActive = false;

            _light.SetActive(IsActive);
            _spriteRenderer.enabled = IsActive;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = IsActive;
            _animator.enabled = IsActive;
        }

        public object CaptureState()
        {
            return new CoinData
            {
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (CoinData)state;

            IsActive = saveData.IsActive;

            _light.SetActive(IsActive);
            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
            _animator.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class CoinData
    {
        public bool IsActive;
    }
}
