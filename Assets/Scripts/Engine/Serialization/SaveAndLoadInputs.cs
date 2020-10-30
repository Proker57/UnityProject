using UnityEngine;

namespace BOYAREngine
{
    public class SaveAndLoadInputs : MonoBehaviour
    {
        private SaveManager _saveManager;
        private LoadManager _loadManager;

        private SaveLoad _saveLoad;

        private void Awake()
        {
            _saveManager = new SaveManager();
            _loadManager = new LoadManager();

            _saveLoad = GetComponent<SaveLoad>();
        }

        private void Start()
        {
            Inputs.Input.Global.Save.started += _ => Save_started();
            Inputs.Input.Global.Load.started += _ => Load_started();
        }

        private void Save_started()
        {
            Events.Save();
            var playerData = FindObjectOfType<Player>().GetComponent<Stats>().PlayerData;
            //_saveManager.Save(playerData);

            _saveLoad.Save();
        }

        private void Load_started()
        {
            Events.Load();
            //_loadManager.Load();

            _saveLoad.Load();
        }

    }
}

