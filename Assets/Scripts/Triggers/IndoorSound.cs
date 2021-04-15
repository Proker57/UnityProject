using UnityEngine;
using UnityEngine.Audio;

namespace BOYAREngine.Sound
{
    public class IndoorSound : MonoBehaviour
    {
        private UnityEngine.Audio.AudioMixer _audioMixer;

        public AudioMixerSnapshot Default;
        public AudioMixerSnapshot Indoor;

        [SerializeField] private float _transitionTime = 2f;

        private void Start()
        {
            _audioMixer = AudioMixer.Instance.MasterMixer;
        }

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;

            Indoor.TransitionTo(_transitionTime);
        }

        private void OnTriggerExit2D(Object other)
        {
            if (other.name != "Low Collider") return;

            Default.TransitionTo(_transitionTime);
        }
    }
}

