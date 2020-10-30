using UnityEngine;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour, ISaveable
    {
        public PlayerData PlayerData = new PlayerData();

        public float XPosition;
        public float YPosition;
        public int Health = 100;
        public int MaxHealth = 100;
        public int Exp = 0;
        public int MaxExp;
        public int Level = 1;

        private void Start()
        {
            MaxExp = (int)(PlayerData.Level * 100 * 1.2f);
        }

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
            MaxExp = (int)(PlayerData.Level * 100 * 1.2f);

            PlayerData.Exp += expValue;

            if (PlayerData.Exp >= MaxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            PlayerData.Level++;
            PlayerData.Exp = 0;
            MaxExp = (int)(PlayerData.Level * 100 * 1.2f);
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
            PlayerEvents.GiveExp += EXPCalculator;
        }

        private void OnDisable()
        {
            Events.Save -= SavePosition;
            Events.Load -= LoadPosition;
            Events.GetDamage -= GetDamage;
            PlayerEvents.GiveExp -= EXPCalculator;
        }

        public object CaptureState()
        {
            return new PlayerData
            {
                XPosition = XPosition,
                YPosition = YPosition,
                Health = Health,
                MaxHealth = MaxHealth,
                Exp = Exp,
                MaxExp = MaxExp,
                Level = Level
            };
        }

        public void RestoreState(object state)
        {
            var playerData = (PlayerData) state;

            XPosition = playerData.XPosition;
            YPosition = playerData.YPosition;
            Health = playerData.Health;
            MaxHealth = playerData.MaxHealth;
            Exp = playerData.Exp;
            MaxExp = playerData.MaxExp;
            Level = playerData.Level;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public float XPosition;
        public float YPosition;

        public int Health;
        public int MaxHealth;
        public int Exp;
        public int MaxExp;
        public int Level;
    }
}



