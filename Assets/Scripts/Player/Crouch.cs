using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.PlayerLoop;

namespace BOYAREngine
{
    public class Crouch : MonoBehaviour
    {
#pragma warning disable 649
        public bool IsCrouched;
        public bool HasCeiling;
        [SerializeField] private float _distance;   // 0.05f
        public Transform LeftCeilingChecker;
        public Transform RightCeilingChecker;
        public LayerMask Ground;

        private Player _player;
#pragma warning restore 649

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _player.Input.PlayerInGame.Crouch.started += _ => Crouch_started();
            _player.Input.PlayerInGame.Crouch.canceled += _ => Crouch_canceled();
        }

        private void FixedUpdate()
        {
            Vector2 leftCeilingOrigin = LeftCeilingChecker.position;
            Vector2 rightCeilingOrigin = RightCeilingChecker.position;
            Vector2 direction = new Vector2(0, _distance);

            // TODO delete debug ray of jump
            Debug.DrawRay(leftCeilingOrigin, direction, Color.red, 0.8f);
            Debug.DrawRay(rightCeilingOrigin, direction, Color.blue, 0.8f);
            RaycastHit2D leftCeilingHit = Physics2D.Raycast(leftCeilingOrigin, direction, _distance, Ground);
            RaycastHit2D rightCeilingHit = Physics2D.Raycast(rightCeilingOrigin, direction, _distance, Ground);
            if (leftCeilingHit.collider != null || rightCeilingHit.collider != null)
            {
                HasCeiling = true;
            }
            else
            {
                HasCeiling = false;
            }

            if (HasCeiling == false && IsCrouched == false)
            {
                IsCrouched = false;
                StopCrouch();
            }
        }

        private void Crouch_started()
        {
            StartCrouch();
        }

        private void Crouch_canceled()
        {
            if (HasCeiling == false)
            {
                StopCrouch();
            }

            IsCrouched = false;
        }

        private void StartCrouch()
        {
            _player.HighCollider.enabled = false;
            IsCrouched = true;
            _player.Animator.SetBool("isCrouch", true);
        }

        private void StopCrouch()
        {
            _player.HighCollider.enabled = true;
            _player.Animator.SetBool("isCrouch", false);
        }

    }
}
