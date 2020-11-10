using UnityEngine;

namespace BOYAREngine
{
    public interface IBreakable
    {
        void GetDamage(int amount);
        void OnBreak();
    }
}
