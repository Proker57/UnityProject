using UnityEngine;

namespace BOYAREngine.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class Chair : MonoBehaviour
    {
        private Enemy _main;

        private void Awake()
        {
            _main = GetComponent<Enemy>();
        }

        public void Attack()
        {
            Debug.Log("Chair is Attacking: " + _main.AttackDamage + " dmg");
        }
    }
}
