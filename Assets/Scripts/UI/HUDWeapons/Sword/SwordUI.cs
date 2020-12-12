using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class SwordUI : MonoBehaviour
    {
        private Image _image;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _inactiveSprite;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (WeaponManager.CurrentWeapon == (int) WeaponEnum.Weapon.Sword)
            {
                _image.sprite = _normalSprite;
            }
            else
            {
                _image.sprite = _inactiveSprite;
            }
        }
    }
}
