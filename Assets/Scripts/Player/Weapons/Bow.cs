namespace BOYAREngine
{
    public class Bow : ISaveable
    {
        public static int Damage = 4;

        public object CaptureState()
        {
            return new BowData
            {
                Damage = Damage
            };
        }

        public void RestoreState(object state)
        {
            var bowData = (BowData)state;

            Damage = bowData.Damage;
        }
    }

    [System.Serializable]
    public class BowData
    {
        public int Damage;
    }
}
