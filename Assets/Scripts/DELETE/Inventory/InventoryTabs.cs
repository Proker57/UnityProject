using System;
using UnityEngine;

namespace BOYAREngine
{
    public class InventoryTabs : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponsActiveTabImage;
        [SerializeField] private GameObject _itemsActiveTabImage;

        public void SetWeaponTab()
        {
            Inventory.Instance.CurrentTab = Inventory.TabType.Weapons;
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);

            SetActiveTab(Inventory.TabType.Weapons);

            InventoryOptions.HighlightChosenWeapon();
        }

        public void SetItemTab()
        {
            Inventory.Instance.CurrentTab = Inventory.TabType.Items;
            Inventory.Instance.UpdateSprites(Inventory.Instance.CurrentTab);

            SetActiveTab(Inventory.TabType.Items);

            InventoryOptions.ClearHighLight();
        }

        public void SetActiveTab(Inventory.TabType type)
        {
            switch (type)
            {
                case Inventory.TabType.Weapons:
                    _weaponsActiveTabImage.SetActive(true);
                    _itemsActiveTabImage.SetActive(false);
                    break;
                case Inventory.TabType.Items:
                    _weaponsActiveTabImage.SetActive(false);
                    _itemsActiveTabImage.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void EnterPointer()
        {
            Attack.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            Attack.Instance.IsAbleToAttack = true;
        }
    }
}
