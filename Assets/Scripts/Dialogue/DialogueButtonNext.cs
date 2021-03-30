using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueButtonNext : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private bool _isActive;

        private void Update()
        {
            if (_isActive)
            {
                _button.image.color = new Color(255f, 255f, 255f, Mathf.PingPong(Time.time * 0.7f, 1f));
            }
        }

        private void OnEnable()
        {
            _isActive = true;
        }

        private void OnDisable()
        {
            _isActive = false;
        }
    }
}

