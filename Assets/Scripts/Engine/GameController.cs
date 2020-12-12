using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOYAREngine
{
    public class GameController : MonoBehaviour, ISaveable
    {
        public static GameController Instance = null;

        public static bool HasPlayer = false;
        public static bool HasCamera = false;

        public string SceneName;

        public static bool IsNewGame = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            DOTween.Init();

            CreatePlayerPrefs();
        }

        private static void CreatePlayerPrefs()
        {
            if (!PlayerPrefs.HasKey("Locale"))
            {
                PlayerPrefs.SetString("Locale", "ru");
            }
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 0f);
            }
            if (!PlayerPrefs.HasKey("SoundVolume"))
            {
                PlayerPrefs.SetFloat("SoundVolume", 0f);
            }
        }

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
            var cinemachineCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0)
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
