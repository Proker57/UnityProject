using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BOYAREngine
{
    public class Door : MonoBehaviour
    {
        private bool _isEnter;
        private float _startPosition;

        [Header("Manual door")]
        [SerializeField] private GameObject _panel;

        [Header("General settings")]
        [SerializeField] private float _animationDuration = 1;
        [SerializeField] private GameObject _door;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private enum Type
        {
            Automatic,
            Manual
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        [SerializeField] private Type _type;
        [SerializeField] private Direction _direction;

        private void Awake()
        {
            _startPosition = _door.transform.position.y;
        }

        private void OnTriggerEnter2D(Object collider)
        {
            if (!collider.name.Equals("Low Collider")) return;
            _isEnter = true;

            switch (_type)
            {
                case Type.Automatic:
                    OpenDoor(true, _direction);
                    break;
                case Type.Manual:
                    ShowPanel(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerExit2D(Object collider)
        {
            if (!collider.name.Equals("Low Collider")) return;
            _isEnter = false;

            switch (_type)
            {
                case Type.Automatic:
                    OpenDoor(false, _direction);
                    break;
                case Type.Manual:
                    ShowPanel(false);
                    OpenDoor(false, _direction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OpenDoor(bool isOpened, Direction direction)
        {
            if (isOpened)
            {
                float endPosition;

                switch (direction)
                {
                    case Direction.Up:
                        endPosition = transform.position.y + _spriteRenderer.bounds.size.y;
                        _door.transform.DOMoveY(endPosition, _animationDuration, false);
                        break;
                    case Direction.Down:
                        endPosition = transform.position.y - _spriteRenderer.bounds.size.y;
                        _door.transform.DOMoveY(endPosition, _animationDuration, false);
                        break;
                    case Direction.Left:
                        // TODO Door left direction
                        break;
                    case Direction.Right:
                        // TODO Door right direction
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
            else
            {
                _door.transform.DOMoveY(_startPosition, _animationDuration, false);
            }
        }

        private void ShowPanel(bool isActive)
        {
            _panel.SetActive(isActive);
        }

        private void Use_started()
        {
            if (_type == Type.Manual && _isEnter)
            {
                _panel.SetActive(false);
                OpenDoor(true, _direction);
            }
        }

        private void OnEnable()
        {
            Player.Instance.Input.PlayerInGame.Use.started += _ => Use_started();
        }

        private void OnDisable()
        {
            Player.Instance.Input.PlayerInGame.Use.started -= _ => Use_started();
        }
    }
}
