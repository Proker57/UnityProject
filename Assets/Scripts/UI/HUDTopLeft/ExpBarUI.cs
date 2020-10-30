using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ExpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Stats _stats;

        private void Update()
        {
            if (_stats == null)
            {
                _stats = FindObjectOfType<Player>().GetComponent<Player>().Stats;
            }

            if (_stats == null) return;
            var currentExp = (float)_stats.Exp;
            var maxExp = (float)_stats.MaxExp;
            _image.fillAmount = currentExp / maxExp;
        }
    }
}
