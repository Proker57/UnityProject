namespace BOYAREngine
{
    public class Events
    {
        public delegate void SaveDelegate();
        public static SaveDelegate Save;

        public delegate void LoadDelegate();
        public static LoadDelegate Load;

        public delegate void PlayerOnSceneDelegate(bool isActive);
        public static PlayerOnSceneDelegate PlayerOnScene;

        public delegate void NewGameDelegate();
        public static NewGameDelegate NewGame;
    }
}

