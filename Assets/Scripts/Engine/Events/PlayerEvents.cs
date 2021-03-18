namespace BOYAREngine
{
    public class PlayerEvents
    {
        public delegate void DashDelegate();
        public static DashDelegate Dash;
        public delegate void DashReadyDelegate();
        public static DashReadyDelegate DashReady;

        public delegate void DoubleJumpDelegate();
        public static DoubleJumpDelegate DoubleJump;
        public delegate void DoubleJumpReadyDelegate();
        public static DoubleJumpReadyDelegate DoubleJumpReady;

        public delegate void JumpDownPlatformDelegate();
        public static JumpDownPlatformDelegate JumpDownPlatform;

        public delegate void LevelUpDelegate();
        public static LevelUpDelegate LevelUp;

        public delegate void GiveExpDelegate(int expValue);
        public static GiveExpDelegate GiveExp;

        public delegate void GiveCurrencyDelegate(int amount);
        public static GiveCurrencyDelegate GiveCurrency;

        public delegate void RestoreHealthDelegate(int amount);
        public static RestoreHealthDelegate RestoreHealth;

        public delegate void DamageDelegate(int damage);
        public static DamageDelegate Damage;

        public delegate void UpdateHPBarDelegate();
        public static UpdateHPBarDelegate UpdateHPBar;

        public delegate void SpendLevelUpPointsDelegate();
        public static SpendLevelUpPointsDelegate SpendLevelUpPoints;
    }
}