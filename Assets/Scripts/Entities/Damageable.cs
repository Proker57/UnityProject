using UnityEngine;

namespace BOYAREngine
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Animator _animator;

        public int Health;

        public void GetDamage(int amount)
        {
            Health -= amount;

            if (_animator != null)
            {
                _animator.SetTrigger("Hit");
            }

            if (_enemy != null)
            {
                _enemy.AudioSource.PlayOneShot(_enemy.HitSound);
            }
        }
    }
}
