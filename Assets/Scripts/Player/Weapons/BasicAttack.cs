using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class BasicAttack : MonoBehaviour
    {
        public bool IsAbleToAttack;

        private Player _player;
        private Camera _camera;
        [Header("Basic settings")]
        [SerializeField] private GameObject _attackPoint;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [Header("Arrow settings")]
        [SerializeField] private GameObject _arrow;
        [SerializeField] private float _arrowSpeed;

        private Vector3 _lookDirection;
        private float _lookAngle;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Attack_started()
        {
            var weapon = _player.WeaponManager;

            if (IsAbleToAttack)
            {
                if (WeaponManager.Instance.CurrentWeapon == (int) WeaponEnum.Weapon.Sword)
                {
                    var hit = Physics2D.OverlapCircleAll(_attackPoint.transform.position, _radius, _layerMask);
                    foreach (var enemies in hit)
                    {
                        enemies.GetComponent<Damageable>().GetDamage(weapon.Damage);
                    }
                }

                if (WeaponManager.Instance.CurrentWeapon == (int)WeaponEnum.Weapon.Bow)
                {
                    if (Bow.Amount > 0)
                    {
                        SpawnArrow();

                        Bow.Amount--;
                    }
                    else
                    {
                        _player.WeaponManager.SetWeapon((int) WeaponEnum.Weapon.Sword);
                    }
                }
            }
        }

        private void SpawnArrow()
        {
            _lookDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var difference = _lookDirection - transform.position;
            _lookAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var distance = difference.magnitude;
            var direction = difference / distance;
            direction.Normalize();

            var arrow = Instantiate(_arrow);
            arrow.transform.position = _attackPoint.transform.position;
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle);
            arrow.GetComponent<Rigidbody2D>().velocity = direction * _arrowSpeed;
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.PrimaryAttack.started += _ => Attack_started();
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.PrimaryAttack.started -= _ => Attack_started();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null) return;

            Gizmos.DrawWireSphere(_attackPoint.transform.position, _radius);
        }
#endif
    }
}
