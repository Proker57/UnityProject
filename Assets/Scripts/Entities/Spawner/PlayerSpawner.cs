using UnityEngine;

namespace BOYAREngine
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private GameObject _player;

        private void Awake()
        {
            if (_player == null)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player");
                }
                else
                {
                    //Destroy(GameObject.FindGameObjectWithTag("Player"));
                    _player = Instantiate(_prefab, transform.position, Quaternion.identity);
                }

                _player.transform.position = transform.position;
            }
            else
            {
                Debug.Log("Else");
                //Destroy(_player);

                if (GameController.IsNewGame)
                {
                    Debug.Log("New Game");
                    Destroy(_player);
                    _player = Instantiate(_prefab, transform.position, Quaternion.identity);
                    _player.transform.position = transform.position;
                }
                else
                {
                    Debug.Log("Not new game");
                    _player = GameObject.FindGameObjectWithTag("Player");
                }
            }

            GameController.HasPlayer = true;
            GameController.SetCameraFollowPlayer();
            //_player.transform.position = transform.position;
        }
    }
}
