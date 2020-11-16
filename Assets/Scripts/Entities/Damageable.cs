using UnityEngine;

namespace BOYAREngine
{
    public class Damageable : MonoBehaviour
    {
        public int Health;

        public void GetDamage(int amount)
        {
            Health -= amount;
        }
    }
}
