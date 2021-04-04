using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace BOYAREngine
{
    public class TorchLightFlicker : MonoBehaviour
    {
        [SerializeField] private float _min = 0.7f;
        [SerializeField] private float _max = 1f;

        private Light2D _light2D;

        private void Awake()
        {
            _light2D = GetComponent<Light2D>();
        }

        private void Update()
        {
            _light2D.intensity = Mathf.Lerp(_min, _max, Mathf.PingPong(Time.time, 1f));
        }
    }
}

