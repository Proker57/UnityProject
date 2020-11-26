﻿using UnityEngine;

namespace BOYAREngine
{
    public class MediumPotion : MonoBehaviour, ISaveable
    {
        public bool IsActive;

        [SerializeField] private GameObject _light;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private Rigidbody2D _rigidbody2D;
        private ItemManager _itemManager;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name != "Low Collider") return;

            ItemEvents.ItemPickUp((int) ItemEnum.ItemType.MediumPotion);

            IsActive = false;

            _light.SetActive(IsActive);
            _spriteRenderer.enabled = IsActive;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = IsActive;
        }

        public object CaptureState()
        {
            return new MediumPotionData
            {
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (MediumPotionData) state;

            IsActive = saveData.IsActive;

            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class MediumPotionData
    {
        public bool IsActive;
    }
}
