using UnityEngine;

namespace BOYAREngine
{
    public class Bow : MonoBehaviour, ISaveable
    {
        public static int Damage = 4;
        public static int Amount = 10;

        public object CaptureState()
        {
            return new BowData
            {
                Damage = Damage,
                Amount = Amount
            };
        }

        public void RestoreState(object state)
        {
            var bowData = (BowData)state;

            Damage = bowData.Damage;
            Amount = bowData.Amount;
        }
    }

    [System.Serializable]
    public class BowData
    {
        public int Damage;
        public int Amount;
    }
}
