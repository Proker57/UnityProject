using UnityEngine;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour, ISaveable
    {
        public float XPosition;
        public float YPosition;
        public float XVelocity;
        public float YVelocity;
        public int Health = 100;
        public int MaxHealth = 100;
        public int Exp = 0;
        public int MaxExp = 120;
        public int Level = 1;
        public int LevelUpPoints = 0;
        public int Currency = 0;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void OnGetExp(int expValue)
        {
            MaxExp = (int)(Level * 100 * 1.2f);
            Exp += expValue;

            if (Exp >= MaxExp)
            {
                PlayerEvents.LevelUp?.Invoke();
            }
        }

        public void OnGiveCurrency(int amount)
        {
            Currency += amount;
        }

        private void OnLevelUp()
        {

            while (Exp >= MaxExp)
            {
                MaxExp = (int)(Level * 100 * 1.2f);
                var balance = Exp - MaxExp;

                Level++;
                LevelUpPoints += 1;

                Exp = 0;
                Exp += balance;
            }

            HUDEvents.LevelUpPointsToggle?.Invoke(true);
            HUDEvents.LevelUpdate?.Invoke(Level);

            MaxHealth = (int)(MaxHealth * 1.2f);

            PlayerEvents.UpdateHPBar?.Invoke();
        }

        private void OnSpendLevelUpPoints()
        {
            LevelUpPoints--;

            if (LevelUpPoints <= 0)
            {
                HUDEvents.LevelUpPointsToggle?.Invoke(false);
            }
        }

        private void OnRestoreHealth(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;

            PlayerEvents.UpdateHPBar?.Invoke();
        }

        private void OnDamage(int damage)
        {
            Health -= damage;

            PlayerEvents.UpdateHPBar?.Invoke();
        }

        private void OnEnable()
        {
            PlayerEvents.GiveExp += OnGetExp;
            PlayerEvents.LevelUp += OnLevelUp;
            PlayerEvents.GiveCurrency += OnGiveCurrency;
            PlayerEvents.RestoreHealth += OnRestoreHealth;
            PlayerEvents.Damage += OnDamage;
            PlayerEvents.SpendLevelUpPoints += OnSpendLevelUpPoints;
        }

        private void OnDisable()
        {
            PlayerEvents.GiveExp -= OnGetExp;
            PlayerEvents.LevelUp -= OnLevelUp;
            PlayerEvents.GiveCurrency -= OnGiveCurrency;
            PlayerEvents.RestoreHealth -= OnRestoreHealth;
            PlayerEvents.Damage -= OnDamage;
            PlayerEvents.SpendLevelUpPoints -= OnSpendLevelUpPoints;
        }

        public object CaptureState()
        {
            return new PlayerData
            {
                // Basic
                XPosition = transform.position.x,
                YPosition = transform.position.y,
                XVelocity = _player.Rigidbody2D.velocity.x,
                YVelocity = _player.Rigidbody2D.velocity.y,
                // Stats
                Health = Health,
                MaxHealth = MaxHealth,
                Exp = Exp,
                MaxExp = MaxExp,
                Level = Level,
                LevelUpPoints = LevelUpPoints,
                Currency = Currency,
                // Dash
                DashTimerCounter = _player.Dash.DashTimerCurrent,
                IsDashable = _player.Dash.IsDashable,
                // jump
//                JumpExtraCounts = _player.JumpOld.JumpExtraCounts,
//                JumpExtraCountDefault = _player.JumpOld.JumpExtraCountDefault,
//                IsJumping = _player.JumpOld.IsJumping,
//                IsDoubleJumping = _player.JumpOld.IsDoubleJumping,
//                IsStoppedJumping = _player.JumpOld.IsStoppedJumping,

                IsJumping = _player.Jump.IsJumping,
                //Crouch
                IsCrouched = _player.Crouch.IsCrouched,
                HasCeiling = _player.Crouch.HasCeiling
            };
        }

        public void RestoreState(object state)
        {
            var playerData = (PlayerData) state;

            // Basic
            XPosition = playerData.XPosition;
            YPosition = playerData.YPosition;
            transform.position = new Vector2(XPosition, YPosition);
            XVelocity = playerData.XVelocity;
            YVelocity = playerData.YVelocity;
            _player.Rigidbody2D.velocity = new Vector2(XVelocity, YVelocity);
            // Stats - stats
            Health = playerData.Health;
            MaxHealth = playerData.MaxHealth;
            Exp = playerData.Exp;
            MaxExp = playerData.MaxExp;
            Level = playerData.Level;
            LevelUpPoints = playerData.LevelUpPoints;
            Currency = playerData.Currency;
            // Dash - dash logic
            _player.Dash.DashTimerCurrent = playerData.DashTimerCounter;
            _player.Dash.IsDashable = playerData.IsDashable;
            // Jump - jumping logic
//            _player.JumpOld.JumpExtraCounts = playerData.JumpExtraCounts;
//            _player.JumpOld.JumpExtraCountDefault = playerData.JumpExtraCountDefault;
//            _player.JumpOld.IsJumping = playerData.IsJumping;
//            _player.JumpOld.IsDoubleJumping = playerData.IsDoubleJumping;
//            _player.JumpOld.IsStoppedJumping = playerData.IsStoppedJumping;

            _player.Jump.IsJumping = playerData.IsJumping;
            // Crouch
            _player.Crouch.IsCrouched = playerData.IsCrouched;
            _player.Crouch.HasCeiling = playerData.HasCeiling;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        // Basic
        public float XPosition;
        public float YPosition;
        public float XVelocity;
        public float YVelocity;
        // Stats
        public int Health;
        public int MaxHealth;
        public int Exp;
        public int MaxExp;
        public int Level;
        public int LevelUpPoints;
        public int Currency;
        // Dash
        public float DashTimerCounter;
        public bool IsDashable;
        // Jump
        public bool IsJumping;
        // Crouch
        public bool IsCrouched;
        public bool HasCeiling;
    }
}