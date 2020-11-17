using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOYAREngine
{
    public class GameController : MonoBehaviour, ISaveable
    {
        public static bool HasPlayer = false;
        public static bool HasCamera = false;

        public string SceneName;

        public static bool IsNewGame = true;

        public object CaptureState()
        {
            SceneName = SceneManager.GetActiveScene().name;

            return new GameControllerData
            {
                SceneName = SceneName
            };
        }

        public void RestoreState(object state)
        {
            var gcData = (GameControllerData) state;

            SceneName = gcData.SceneName;
            SceneLoader.SwitchScene(SceneName);
        }

        public static void SetCameraFollowPlayer()
        {
            var cinemachineCamera = GameObject.FindGameObjectWithTag("MainCamera")
                .GetComponent<CinemachineVirtualCamera>();
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            cinemachineCamera.Follow = player;
        }
    }

    [System.Serializable]
    public class GameControllerData
    {
        public string SceneName;
    }
}
