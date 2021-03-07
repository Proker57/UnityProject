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
        public enum MeleeActions
        {
            DoNothing,
            Attack,
            JumpBack,
            Count
        }
        private MeleeActions _meleeActions;
        private const int MeleeCount = (int) MeleeActions.Count;

        public enum RangeActions
        {
            DoNothing,
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

        public void ChooseMeleeAction(int action)
        {
            if (IsRandomMeleeActions)
            {
                _meleeActions = (MeleeActions) Random.Range(0, MeleeCount);
            }
            else
            {
                _meleeActions = (MeleeActions) action;
            }
            Debug.Log(_meleeActions);

            switch (_meleeActions)
            {
                case MeleeActions.DoNothing:
                    break;
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
                _rangeActions = (RangeActions) Random.Range(0, RangeCount);
            }
            else
            {
                _rangeActions = (RangeActions) action;
            }
            Debug.Log(_rangeActions);

            switch (_rangeActions)
            {
                case RangeActions.DoNothing:
                    break;
                case RangeActions.Dash:
                    _aiBaseActions.Dash();
                    break;
                case RangeActions.Count:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
