using UnityEngine;

namespace BOYAREngine
{
    public class SoundVolume : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _audioSource.volume = GameController.Instance.MusicVolume;
        }
    }
}
