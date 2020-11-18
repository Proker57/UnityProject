namespace BOYAREngine
{
    public class Sword : ISaveable
    {
        public int Damage = 10;

        public object CaptureState()
        {
            return new SwordData
            {
                Damage = Damage
            };
        }

        public void RestoreState(object state)
        {
            var swordData = (SwordData) state;

            Damage = swordData.Damage;
        }
    }

    [System.Serializable]
    public class SwordData
    {
        public int Damage;
    }
}
