using UnityEngine;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour
    {
        public PlayerData PlayerData = new PlayerData();

        private void SavePosition()
        {
            PlayerData.XPosition = transform.position.x;
            PlayerData.YPosition = transform.position.y;
        }

        private void LoadPosition()
        {
            var position = new Vector2(PlayerData.XPosition, PlayerData.YPosition);
            transform.position = position;
        }

        public void EXPCalculator(int expValue)
        {
            var maxExp = (int) (PlayerData.Level * 100 * 1.2f);

            PlayerData.EXP += expValue;

            if (PlayerData.EXP >= maxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            PlayerData.Level++;
            PlayerData.MaxHealth = (int) (PlayerData.MaxHealth * 1.2f);
        }

        private void GetDamage(int damageValue)
        {
            PlayerData.Health -= damageValue;
        }

        private void OnEnable()
        {
            Events.Save += SavePosition;
            Events.Load += LoadPosition;
            Events.GetDamage += GetDamage;
            Events.GetXp += EXPCalculator;
        }

        private void OnDisable()
        {
            Events.Save -= SavePosition;
            Events.Load -= LoadPosition;
            Events.GetDamage -= GetDamage;
            Events.GetXp -= EXPCalculator;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public float XPosition;
        public float YPosition;

        public int Health = 100;
        public int MaxHealth = 100;
        public int Level = 1;
        public int EXP = 0;
    }
}



