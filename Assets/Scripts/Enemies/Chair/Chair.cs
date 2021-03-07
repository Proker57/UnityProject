using System;
using BOYAREngine.Enemies.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BOYAREngine.Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(AIBaseActions))]
    public class Chair : MonoBehaviour
    {
        [Header("Melee chances")]
        [Range(0.0f, 1.0f)] [SerializeField] private float _attack = 0.5f;
        [Range(0.0f, 1.0f)] [SerializeField] private float _jumpBack = 0.4f;
        [SerializeField] private float _doNothingMelee;
        public enum MeleeActions
        {
            Attack,
            JumpBack,

            Count
        }
        private MeleeActions _meleeActions;
        private const int MeleeCount = (int) MeleeActions.Count;

        [Header("Range chances")]
        [Range(0.0f, 1.0f)] [SerializeField] private float _dash = 0.3f;
        [SerializeField] private float _doNothingRange;
        public enum RangeActions
        {
            Dash,

            Count
        }
        private RangeActions _rangeActions;
        private const int RangeCount = (int) RangeActions.Count;

        public bool IsRandomMeleeActions = true;
        public bool IsRandomRangeActions = true;

        private AIBaseActions _aiBaseActions;

        private void Awake()
        {
            _aiBaseActions = GetComponent<AIBaseActions>();
        }

        private void Start()
        {
            OnValidate();
        }

        public void ChooseMeleeAction(int action)
        {
            if (IsRandomMeleeActions)
            {
                var randomIndex = Random.value;

                if (randomIndex >= 0.0f && randomIndex <= _attack)
                {
                    _meleeActions = MeleeActions.Attack;
                } else 
                if (randomIndex > _attack && randomIndex <= _attack + _jumpBack)
                {
                    _meleeActions = MeleeActions.JumpBack;
                }
            }
            else
            {
                _meleeActions = (MeleeActions) action;
            }

            switch (_meleeActions)
            {
                case MeleeActions.Attack:
                    _aiBaseActions.Attack();
                    break;
                case MeleeActions.JumpBack:
                    _aiBaseActions.JumpBack();
                    break;
                case MeleeActions.Count:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChooseRangeActions(int action)
        {
            if (IsRandomRangeActions)
            {
                var randomIndex = Random.value;

                if (randomIndex >= 0.0f && randomIndex <= _dash)
                {
                    _rangeActions = RangeActions.Dash;
                }
            }
            else
            {
                _rangeActions = (RangeActions) action;
            }

            switch (_rangeActions)
            {
                case RangeActions.Dash:
                    _aiBaseActions.Dash();
                    break;
                case RangeActions.Count:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnValidate()
        {
            _doNothingMelee = 1.0f - (_attack + _jumpBack);
            _doNothingRange = 1.0f - (_dash);
        }
    }
}
