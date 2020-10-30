using UnityEngine;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour, ISaveable
    {
        public float XPosition;
        public float YPosition;
        public int Health = 100;
        public int MaxHealth = 100;
        public int Exp = 0;
        public int MaxExp = 120;
        public int Level = 1;

        public void GetExp(int expValue)
        {
            MaxExp = (int)(Level * 100 * 1.2f);

            Exp += expValue;

            if (Exp >= MaxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Exp = 0;
            MaxExp = (int)(Level * 100 * 1.2f);
            MaxHealth = (int) (MaxHealth * 1.2f);
        }

        private void GetDamage(int damageValue)
        {
            Health -= damageValue;
        }

        private void OnEnable()
        {
            Events.GetDamage += GetDamage;
            PlayerEvents.GiveExp += GetExp;
        }

        private void OnDisable()
        {
            Events.GetDamage -= GetDamage;
            PlayerEvents.GiveExp -= GetExp;
        }

        public object CaptureState()
        {
            return new PlayerData
            {
                XPosition = transform.position.x,
                YPosition = transform.position.y,
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
            transform.position = new Vector2(XPosition, YPosition); ;
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



