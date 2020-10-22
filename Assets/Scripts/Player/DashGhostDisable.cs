using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class DashGhostDisable : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
