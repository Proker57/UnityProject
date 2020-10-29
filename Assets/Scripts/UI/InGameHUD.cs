using BOYAREngine;
using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    private SceneLoader _sceneLoader;
    private UIManagerLegacy _uiManager;

    private void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
        _uiManager = UI.GetComponent<UIManagerLegacy>();
    }

    private void Update()
    {
        if (_sceneLoader._currentSceneName.Equals("Main") || _sceneLoader._currentSceneName.Equals("MainMenu"))
        {
            _uiManager._hud.SetActive(false);
        }
        else
        {
            _uiManager._hud.SetActive(true);
        }
    }
}
