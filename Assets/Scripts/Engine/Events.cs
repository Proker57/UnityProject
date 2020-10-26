namespace BOYAREngine
{
    public class Events
    {
        public delegate void SaveDelegate();
        public static SaveDelegate Save;

        public delegate void LoadDelegate();
        public static LoadDelegate Load;

        public delegate void DashDelegate(float time);
        public static DashDelegate Dash;
        public delegate void DashReadyDelegate();
        public static DashReadyDelegate DashReady;

        public delegate void DoubleJumpDelegate();
        public static DoubleJumpDelegate DoubleJump;
        public delegate void DoubleJumpReadyDelegate();
        public static DoubleJumpReadyDelegate DoubleJumpReady;

        public delegate void GetXpDelegate(int expValue);
        public static GetXpDelegate GetXp;

        public delegate void GetDamageDelegate(int damageValue);
        public static GetDamageDelegate GetDamage;
    }
}

