using UnityEngine;

namespace BOYAREngine
{
    public class Sword : MonoBehaviour, ISaveable
    {
        public static int Level = 1;
        public static int Damage = 10;

        public object CaptureState()
        {
            return new SwordData
            {
                Level = Level,
                Damage = Damage
            };
        }

        public void RestoreState(object state)
        {
            var swordData = (SwordData) state;

            Level = swordData.Level;
            Damage = swordData.Damage;
        }
    }

    [System.Serializable]
    public class SwordData
    {
        public int Level;
        public int Damage;
    }
}
