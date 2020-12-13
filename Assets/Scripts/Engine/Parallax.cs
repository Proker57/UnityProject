using UnityEngine;

namespace BOYAREngine
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Vector2 _parallaxMultiplier;

        private Transform _cameraTransform;
        private Vector3 _lastCameraPosition;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            _lastCameraPosition = _cameraTransform.position;
        }

        private void LateUpdate()
        {
            var deltaMovement = _cameraTransform.position - _lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * _parallaxMultiplier.x,
                deltaMovement.y * _parallaxMultiplier.y);
            _lastCameraPosition = _cameraTransform.position;
        }
    }
}
