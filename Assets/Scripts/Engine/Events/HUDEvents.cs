namespace BOYAREngine
{
    // ReSharper disable once InconsistentNaming
    public class HUDEvents
    {
        public delegate void DashCheckIsActiveDelegate(bool boolean);
        public static DashCheckIsActiveDelegate DashCheckIsActive;

        public delegate void JumpCheckIsActiveDelegate(bool boolean);
        public static JumpCheckIsActiveDelegate JumpCheckIsActive;

        public delegate void LevelUpPointsToggleDelegate(bool boolean);
        public static LevelUpPointsToggleDelegate LevelUpPointsToggle;

        public delegate void LevelUpdateDelegate(int level);
        public static LevelUpdateDelegate LevelUpdate;
    }
}