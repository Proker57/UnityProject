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

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

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
            MaxHealth = (int)(MaxHealth * 1.2f);
        }

        private void OnEnable()
        {
            PlayerEvents.GiveExp += GetExp;
        }

        private void OnDisable()
        {
            PlayerEvents.GiveExp -= GetExp;
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
                // Dash
                DashTimerCounter = _player.Dash.DashTimerCounter,
                SpeedLimiterTimerCounter = _player.Dash.SpeedLimiterTimerCounter,
                IsDashable = _player.Dash.IsDashable,
                IsSpeedLimited = _player.Dash.IsSpeedLimited,
                // jump
                JumpExtraCounts = _player.Jump.JumpExtraCounts,
                JumpExtraCountDefault = _player.Jump.JumpExtraCountDefault,
                IsJumping = _player.Jump.IsJumping,
                IsDoubleJumping = _player.Jump.IsDoubleJumping,
                IsStoppedJumping = _player.Jump.IsStoppedJumping,
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
            // Dash - dash logic
            _player.Dash.DashTimerCounter = playerData.DashTimerCounter;
            _player.Dash.SpeedLimiterTimerCounter = playerData.SpeedLimiterTimerCounter;
            _player.Dash.IsDashable = playerData.IsDashable;
            _player.Dash.IsSpeedLimited = playerData.IsSpeedLimited;
            // Jump - jumping logic
            _player.Jump.JumpExtraCounts = playerData.JumpExtraCounts;
            _player.Jump.JumpExtraCountDefault = playerData.JumpExtraCountDefault;
            _player.Jump.IsJumping = playerData.IsJumping;
            _player.Jump.IsDoubleJumping = playerData.IsDoubleJumping;
            _player.Jump.IsStoppedJumping = playerData.IsStoppedJumping;
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
        // Dash
        public float DashTimerCounter;
        public float SpeedLimiterTimerCounter;
        public bool IsDashable;
        public bool IsSpeedLimited;
        // Jump
        public int JumpExtraCounts;
        public int JumpExtraCountDefault;
        public bool IsJumping;
        public bool IsDoubleJumping;
        public bool IsStoppedJumping;
        // Crouch
        public bool IsCrouched;
        public bool HasCeiling;
    }
}