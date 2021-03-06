using UnityEngine;

namespace BOYAREngine.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class Chair : MonoBehaviour
    {
        [SerializeField] private float Radius = 1f;
        [SerializeField] private LayerMask _playerLayer;
        private Enemy _main;

        private void Awake()
        {
            _main = GetComponent<Enemy>();
        }

        public void BasicAttack()
        {
            _main.Animator.SetTrigger("BasicAttack");

            var hit = Physics2D.OverlapCircleAll(_main._aiBase.AttackPoint.position, Radius, _playerLayer);
            if (hit != null)
            {
                PlayerEvents.Damage(_main.AttackDamage);
            }

            Debug.Log("Chair is Attacking: " + _main.AttackDamage + " dmg");
        }
    }
}
