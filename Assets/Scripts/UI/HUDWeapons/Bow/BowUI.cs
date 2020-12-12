using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class BowUI : MonoBehaviour
    {
        private Image _image;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _inactiveSprite;
        [SerializeField] private Text _amountText;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (WeaponManager.CurrentWeapon == (int)WeaponEnum.Weapon.Bow)
            {
                _image.sprite = _normalSprite;
            }
            else
            {
                _image.sprite = _inactiveSprite;
            }

            _amountText.text = Bow.Amount.ToString();
        }
    }
}