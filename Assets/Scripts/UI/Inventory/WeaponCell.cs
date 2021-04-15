using UnityEngine;
using UnityEngine.EventSystems;

namespace BOYAREngine.UI
{
    public class WeaponCell : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private WeaponsHover _weaponsHover;

        [HideInInspector] public int CellIndex;

        private void Awake()
        {
            _weaponsHover = GetComponent<WeaponsHover>();
            CellIndex = int.Parse(name);
        }

        public void OnClick()
        {
            if (WeaponManager.Instance.Weapons.Count > CellIndex)
            {
                WeaponManager.Instance.SetWeapon(CellIndex);
            }
        }

        public void HoverPanel(bool isOn)
        {
            if (WeaponManager.Instance.Weapons.Count > CellIndex)
            {
                _weaponsHover.ShowPanel(isOn);
            }
            
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (WeaponManager.Instance.Weapons.Count > CellIndex)
            {
                _weaponsHover.ShowPanel(true);
            }
            
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (WeaponManager.Instance.Weapons.Count > CellIndex)
            {
                _weaponsHover.ShowPanel(false);
            }
        }
    }
}

