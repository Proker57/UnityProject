using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUpPoints;
        [SerializeField] private Text _levelUpPointsText;
        [SerializeField] private Text _level;

        private Player _player;

        private void LevelUpdate(int level)
        {
            if (_player == null) return;
            UpdateLevelStats();
        }

        private void LevelUpPointsToggle(bool isActive)
        {
            _levelUpPoints.SetActive(isActive);
            UpdateLevelStats();
        }

        private void UpdateLevelStats()
        {
            _level.text = "Lv." + _player.Stats.Level;
            _levelUpPointsText.text = "LP:" + _player.Stats.LevelUpPoints;
        }

        private void OnEnable()
        {
            HUDEvents.LevelUpdate += LevelUpdate;
            HUDEvents.LevelUpPointsToggle += LevelUpPointsToggle;

            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            HUDEvents.LevelUpdate -= LevelUpdate;
            HUDEvents.LevelUpPointsToggle -= LevelUpPointsToggle;

            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            UpdateLevelStats();
        }
    }
}