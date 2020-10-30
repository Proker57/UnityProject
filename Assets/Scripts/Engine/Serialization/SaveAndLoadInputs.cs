using UnityEngine;

namespace BOYAREngine
{
    public class SaveAndLoadInputs : MonoBehaviour
    {
        private SaveLoad _saveLoad;

        private void Awake()
        {

            _saveLoad = GetComponent<SaveLoad>();
        }

        private void Start()
        {
            Inputs.Input.Global.Save.started += _ => Save_started();
            Inputs.Input.Global.Load.started += _ => Load_started();
        }

        private void Save_started()
        {
            _saveLoad.Save();
        }

        private void Load_started()
        {
            _saveLoad.Load();
        }

    }
}

