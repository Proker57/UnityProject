using UnityEngine;

namespace BOYAREngine
{
    public class SaveAndLoadInputs : MonoBehaviour
    {
        private SaveLoad _saveLoad;
        private PlayerInput _inputs;

        private void Awake()
        {
            _saveLoad = GetComponent<SaveLoad>();
            _inputs = new PlayerInput();
        }

        private void Save_started()
        {
            _saveLoad.Save();
        }

        private void Load_started()
        {
            _saveLoad.Load();
        }

        private void OnEnable()
        {
            _inputs.Enable();
            _inputs.Global.Save.started += _ => Save_started();
            _inputs.Global.Load.started += _ => Load_started();
        }

        private void OnDisable()
        {
            _inputs.Disable();
            _inputs.Global.Save.started -= _ => Save_started();
            _inputs.Global.Load.started -= _ => Load_started();
        }
    }
}

