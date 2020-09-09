using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _sceneLoader = null;

    [SerializeField] private string _nextSceneName;
    [Header("Loading Screen fields")]
    [Tooltip("Add Loading Screen Game Object from 'Main' scene")] [SerializeField]
    private GameObject _loadingScreen;
    [Tooltip("Add Progress Bar Slider from 'Main' scene")] [SerializeField]
    private Slider _progressBar;
    [Tooltip("Add Circle Sprite from 'Main' scene")] [SerializeField]
    private GameObject _loadingCircleSprite;

    private string _currentSceneName;
    private AsyncOperation _resourceUnloadTaskAsync;
    private AsyncOperation _sceneLoadTaskAsync;

    private enum SceneState
    {
        Reset,
        Preload,
        Load,
        Unload,
        Postload,
        Ready,
        Run,
        Count
    };

    private SceneState _sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] _updateDelegates;

    public static void SwitchScene(string nextSceneName)
    {
        if (_sceneLoader == null) return;
        if(_sceneLoader._currentSceneName != nextSceneName)
        {
            _sceneLoader._nextSceneName = nextSceneName;
        }
    }

    protected void Awake()
    {
        if (_sceneLoader == null)
        {
            _sceneLoader = this;
        }
        else if (_sceneLoader == this)
        {
            Destroy(gameObject);
        }

        _loadingScreen?.SetActive(false);

        _updateDelegates = new UpdateDelegate[(int)SceneState.Count];
        _updateDelegates[(int) SceneState.Reset] = UpdateSceneReset;
        _updateDelegates[(int) SceneState.Preload] = UpdateScenePreload;
        _updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        _updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        _updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        _updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        _updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        _sceneState = SceneState.Reset;
    }

    protected void OnDestroy()
    {
        if (_updateDelegates != null)
        {
            for (var i = 0; i < (int)SceneState.Count; i++)
            {
                _updateDelegates[i] = null;
            }

            _updateDelegates = null;
        }

        if (_sceneLoader != null)
        {
            _sceneLoader = null;
        }
    }

    protected void Update()
    {
        _updateDelegates[(int)_sceneState]?.Invoke();
    }

    private void UpdateSceneReset()
    {
        System.GC.Collect();
        _sceneState = SceneState.Preload;
    }

    private void UpdateScenePreload()
    {
        _sceneLoadTaskAsync = SceneManager.LoadSceneAsync(_nextSceneName);
        _sceneState = SceneState.Load;
    }

    private void UpdateSceneLoad()              // Loading menu
    {
        _sceneLoadTaskAsync.allowSceneActivation = false;
        _progressBar.value = _sceneLoadTaskAsync.progress;

        if (_sceneLoadTaskAsync.isDone == true)
        {
            _loadingScreen.SetActive(false);
            _sceneState = SceneState.Unload;
        }
        else
        {
            _loadingScreen.SetActive(true);

            if (_sceneLoadTaskAsync.progress >= .9f && _sceneLoadTaskAsync.allowSceneActivation == false)
            {
                _loadingCircleSprite.GetComponent<Animation>().Stop();
                _sceneLoadTaskAsync.allowSceneActivation = true;
            }
        }
    }

    private void UpdateSceneUnload()
    {
        if (_resourceUnloadTaskAsync == null)
        {
            _resourceUnloadTaskAsync = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (_resourceUnloadTaskAsync.isDone != true) return;
            _resourceUnloadTaskAsync = null;
            _sceneState = SceneState.Postload;
        }
    }

    private void UpdateScenePostload()
    {
        _currentSceneName = _nextSceneName;
        _sceneState = SceneState.Ready;
    }

    private void UpdateSceneReady()
    {
        System.GC.Collect();
        _sceneState = SceneState.Run;
    }

    private void UpdateSceneRun()
    {
        if (_currentSceneName != _nextSceneName)
        {
            _sceneState = SceneState.Reset;
        }
    }
}