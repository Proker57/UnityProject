using Pathfinding;
using UnityEngine;

namespace BOYAREngine.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [Header("Init")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private CircleCollider2D _circleCollider2D;
        [SerializeField] private Animator _animator;

        [Header("A* Pathfinder")]
        [SerializeField] private AIDestinationSetter _aiDestinationSetter;
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Transform _basePosition;

        [Header("Sound")]
        public AudioSource AudioSource;
        public AudioClip HitSound;
        public AudioClip IdleSound;

        [Header("Vars")]
        public int Health = 100;
        public int AttackDamage = 1;

        public float Speed = 400;
        public float SightRadius = 3;

        [Header("Logic")]
        public bool IsFighting = false;

        private void FixedUpdate()
        {
            // TODO FlipX
            if (_rigidbody2D.velocity.x > 0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (_rigidbody2D.velocity.x < -0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = true;

            if (!AudioSource.isPlaying)
            {
                PlaySound(IdleSound);
            }

            _aiDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;
            //LookAtPlayer(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = false;

            if (AudioSource.isPlaying)
            {
                StopSound();
            }

            _aiDestinationSetter.target = _basePosition;
        }

        public void LookAtPlayer(Collider2D collision)
        {
            if (transform.position.x > collision.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            AudioSource.PlayOneShot(clip);
        }

        private void StopSound()
        {
            AudioSource.Stop();
        }

        public void GetDamage(int amount)
        {
            Health -= amount;

            _animator.SetTrigger("Hit");

            AudioSource.PlayOneShot(HitSound);
        }
    }
}
