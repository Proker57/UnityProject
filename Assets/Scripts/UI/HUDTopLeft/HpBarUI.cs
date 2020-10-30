using System;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        private Stats _stats;

        private void Update()
        {
            if (_stats == null)
            {
                _stats = FindObjectOfType<Player>().GetComponent<Player>().Stats;
            }

            if (_stats == null) return;
            var currentHealth = _stats.Health;
            var maxHealth = _stats.MaxHealth;
            var fillHealthValue = (float) currentHealth / (float) maxHealth;

            _image.fillAmount = fillHealthValue;
            _text.text = currentHealth + "/" + maxHealth;
        }
    }
}
