using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Attack : MonoBehaviour
    {
        public static Attack Instance;

        public Animator Animator;
        public Transform AttackPoint;
        public LayerMask DamageLayers;
        public bool IsAbleToAttack = true;

        private int _combo;

        private float _resetTimerCurrent;
        private float _resetTimer = 3;
        private bool _isComboBraked;

        [SerializeField] private InputActionAsset _controls;
        private Player _player;

        private void Awake()
        {
            if (Instance == null) Instance = this;

            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _controls.FindActionMap("PlayerInGame").FindAction("PrimaryAttack").started += PrimaryAttack_started;
        }

        private void Update()
        {
            if (!_isComboBraked)
            {
                _resetTimerCurrent -= Time.deltaTime;

                if (_resetTimerCurrent <= 0)
                {
                    Debug.Log("Reset Combo");
                    if (WeaponManager.Instance.CurrentWeapon > -1)
                    {
                        WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].Reset();
                    }
                    _combo = 0;
                    _isComboBraked = true;
                }
            }
        }

        private void PrimaryAttack_started(InputAction.CallbackContext obj)
        {
            if (IsAbleToAttack)
            {
                if (WeaponManager.Instance.CurrentWeapon > -1)
                {
                    switch (_combo)
                    {
                        case 0:
                            Invoke("AttackCooldown", WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].AttackSpeed);
                            AttackOverlap(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].FirstAttack());
                            if (_player.Movement.IsLookingRight)
                            {
                                _player.Rigidbody2D.AddForce(new Vector2(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].PushForce, 0), ForceMode2D.Impulse);
                            }
                            else
                            {
                                _player.Rigidbody2D.AddForce(new Vector2(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].PushForce * -1, 0), ForceMode2D.Impulse);
                            }
                            Animator.SetTrigger("PrimaryAttackSword");
                            _combo = 1;
                            break;
                        case 1:
                            Invoke("AttackCooldown", WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].AttackSpeed);
                            AttackOverlap(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].SecondAttack());
                            Animator.SetTrigger("PrimaryAttackSword");
                            _combo = 2;
                            break;
                        case 2:
                            Invoke("AttackCooldown", WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].AttackSpeed);
                            AttackOverlap(WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].ThirdAttack());
                            Animator.SetTrigger("PrimaryAttackSword");
                            _combo = 0;
                            break;
                        default:
                            IsAbleToAttack = true;
                            _combo = 0;
                            break;
                    }
                }

                _resetTimerCurrent = _resetTimer;
                _isComboBraked = false;

                IsAbleToAttack = false;
            }
        }

        private void AttackOverlap(int damage)
        {
            var hit = Physics2D.OverlapCircleAll(AttackPoint.transform.position,
                WeaponManager.Instance.Weapons[WeaponManager.Instance.CurrentWeapon].Radius, DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<IDamageable>().GetDamage(damage);
            }
        }

        private void AttackCooldown()
        {
            IsAbleToAttack = true;
        }
    }
}

