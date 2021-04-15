using BOYAREngine;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 0.6f;
    private float _speedInWater;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Low Collider")
        {
            if (_player == null) _player = other.GetComponentInParent<Player>();
            _speedInWater = _player.Movement.CurrentSpeed * _speedMultiplier;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Low Collider")
        {
            _player.Movement.CurrentSpeed = _speedInWater;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Low Collider")
        {
            _player.Movement.ReturnBaseSpeed();
            _player.WetFx.Play();
        }
    }
}
