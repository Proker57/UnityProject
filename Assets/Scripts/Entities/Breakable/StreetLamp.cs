using UnityEngine;

namespace BOYAREngine
{
    public class StreetLamp : MonoBehaviour, ISaveable, IBreakable
    {
        public bool IsActive;
        public int Health;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (Health <= 0)
            {
                OnBreak();
            }
        }

        public object CaptureState()
        {
            return new StreetLampData
            {
                Health = Health,
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveLampData = (StreetLampData) state;

            IsActive = saveLampData.IsActive;
            Health = saveLampData.Health;

            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }

        public void GetDamage(int amount)
        {
            Health -= amount;
        }

        public void OnBreak()
        {
            IsActive = false;

            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class StreetLampData
    {
        public bool IsActive;
        public int Health;
    }
}
