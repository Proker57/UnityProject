using UnityEngine;

namespace BOYAREngine
{
    public class PlayerEvents : MonoBehaviour
    {
        public delegate void DashDelegate(float time);
        public static DashDelegate Dash;
        public delegate void DashReadyDelegate();
        public static DashReadyDelegate DashReady;

        public delegate void DoubleJumpDelegate();
        public static DoubleJumpDelegate DoubleJump;
        public delegate void DoubleJumpReadyDelegate();
        public static DoubleJumpReadyDelegate DoubleJumpReady;

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
    }
}