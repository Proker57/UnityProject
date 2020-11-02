using UnityEngine;

namespace BOYAREngine
{
    public class TestLevel001 : MonoBehaviour
    {
        private GameController _gameController;

        private void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        private void Start()
        {
            if (_gameController.IsNewGame == false)
            {
                var saveLoad = _gameController.GetComponent<SaveLoad>();
                saveLoad.Load();
            }
        }
    }
}
