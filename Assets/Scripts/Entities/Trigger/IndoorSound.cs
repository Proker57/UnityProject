using UnityEngine;

namespace BOYAREngine.Sound
{
    public class IndoorSound : MonoBehaviour
    {
        private UnityEngine.Audio.AudioMixer _audioMixer;

        private void Start()
        {
            _audioMixer = AudioMixer.Instance.MasterMixer;
        }

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;


            //_audioMixer.TransitionToSnapshots(_audioMixer.FindSnapshot("Indoor"), WeightedMode.In, );
        }
    }
}

