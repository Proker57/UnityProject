using Cinemachine;
using UnityEngine;

namespace BOYAREngine
{
    public class CinemachineFindPlayer : MonoBehaviour
    {
        private void Awake()
        {
            GameController.HasCamera = true;
        }

        private void OnDestroy()
        {
            GameController.HasCamera = false;
        }
    }
}
