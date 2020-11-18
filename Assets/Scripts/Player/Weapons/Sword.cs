using UnityEngine;

namespace BOYAREngine
{
    public class Sword : MonoBehaviour, ISaveable
    {
        public static int Damage = 10;

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
