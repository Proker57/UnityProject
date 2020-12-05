using UnityEngine;
using UnityEngine.Audio;

namespace BOYAREngine
{
    public class AudioMixerVolume : MonoBehaviour
    {
        public static AudioMixerVolume Instance = null;

        public AudioMixer MasterMixer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        public void SetVolume(string key, float soundLevel)
        {
            Mathf.Clamp(soundLevel, -80, 0);

            MasterMixer.SetFloat(key, soundLevel);
        }
    }
}
