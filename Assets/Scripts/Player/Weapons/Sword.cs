using UnityEngine;

namespace BOYAREngine
{
    public class Sword : MonoBehaviour, ISaveable
    {
        public static string Name = "Sword";
        public static int Level = 1;
        public static int Damage = 10;

        public object CaptureState()
        {
            return new SwordData
            {
                Name = Name,
                Level = Level,
                Damage = Damage
            };
        }

        public void RestoreState(object state)
        {
            var swordData = (SwordData) state;

            Name = swordData.Name;
            Level = swordData.Level;
            Damage = swordData.Damage;
        }
    }

    [System.Serializable]
    public class SwordData
    {
        public string Name;
        public int Level;
        public int Damage;
    }
}
