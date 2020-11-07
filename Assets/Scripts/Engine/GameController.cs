using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOYAREngine
{
    public class GameController : MonoBehaviour, ISaveable
    {
        public string SceneName;

        public bool IsNewGame = true;

        public string GetSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public object CaptureState()
        {
            return new GameControllerData
            {
                SceneName = SceneManager.GetActiveScene().name
            };
        }

        public void RestoreState(object state)
        {
            var gcData = (GameControllerData) state;

            SceneName = gcData.SceneName;
        }
    }

    [System.Serializable]
    public class GameControllerData
    {
        public string SceneName;
    }
}
