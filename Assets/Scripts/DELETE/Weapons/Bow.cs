using UnityEngine;

namespace BOYAREngine
{
    public class Bow : MonoBehaviour, ISaveable
    {
        public static int Level = 1;
        public static int Damage = 4;
        public static int Amount = 10;

        public object CaptureState()
        {
            return new BowData
            {
                Level = Level,
                Damage = Damage,
                Amount = Amount
            };
        }

        public void RestoreState(object state)
        {
            var bowData = (BowData)state;

            Level = bowData.Level;
            Damage = bowData.Damage;
            Amount = bowData.Amount;
        }
    }

    [System.Serializable]
    public class BowData
    {
        public int Level;
        public int Damage;
        public int Amount;
    }
}
