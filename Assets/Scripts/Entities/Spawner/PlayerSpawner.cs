using UnityEngine;

namespace BOYAREngine
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private GameObject _player;
        private GameController _gameController;

        private void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        private void Start()
        {
            if (_player == null)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player");
                }
                else
                {
                    _player = Instantiate(_prefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (_gameController.IsNewGame)
                {
                    Destroy(_player);
                    _player = Instantiate(_prefab, transform.position, Quaternion.identity);
                }
            }

            GameController.HasPlayer = true;
            GameController.SetCameraFollowPlayer();
            _player.transform.position = transform.position;
        }
    }
}
