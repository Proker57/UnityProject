using UnityEngine;

namespace BOYAREngine
{
    public class BasicAttack : MonoBehaviour
    {
        public bool IsAbleToAttack;

        private Player _player;
        [SerializeField] private GameObject _attackPoint;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Attack_started()
        {
            var weapon = _player.WeaponManager;

            if (IsAbleToAttack)
            {
                var hit = Physics2D.OverlapCircleAll(_attackPoint.transform.position, _radius, _layerMask);
                
                foreach (var enemies in hit)
                {
                    enemies.GetComponent<Damageable>().GetDamage(weapon.Damage);
                }

                Debug.Log(weapon.Damage);
            }
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.Attack.started += _ => Attack_started();
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.Attack.started -= _ => Attack_started();
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null) return;

            Gizmos.DrawWireSphere(_attackPoint.transform.position, _radius);
        }
    }
}
