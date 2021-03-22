using UnityEngine;

namespace BOYAREngine.Sound
{
    public class AudioMixer : MonoBehaviour
    {
        public static AudioMixer Instance = null;

        public UnityEngine.Audio.AudioMixer MasterMixer;

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
