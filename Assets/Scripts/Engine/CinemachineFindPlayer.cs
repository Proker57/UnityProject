using Cinemachine;
using UnityEngine;

namespace BOYAREngine
{
    public class CinemachineFindPlayer : MonoBehaviour
    {
        private void Start()
        {
            var camera = GetComponent<CinemachineVirtualCamera>();
            camera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
