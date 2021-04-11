using UnityEngine;

namespace BOYAREngine.UI
{
    public class Tabs : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] private GameObject _weaponsIndicator;
        [SerializeField] private GameObject _weaponsOptions;
        [SerializeField] private GameObject _weaponsGrid;
        [Header("Items")]
        [SerializeField] private GameObject _itemsIndicator;
        [SerializeField] private GameObject _itemsOptions;
        [SerializeField] private GameObject _itemsGrid;

        public void Weapons()
        {
            _weaponsIndicator.SetActive(true);
            _weaponsOptions.SetActive(true);
            _weaponsGrid.SetActive(true);

            _itemsIndicator.SetActive(false);
            _itemsOptions.SetActive(false);
            _itemsGrid.SetActive(false);
        }

        public void Items()
        {
            _weaponsIndicator.SetActive(false);
            _weaponsOptions.SetActive(false);
            _weaponsGrid.SetActive(false);

            _itemsIndicator.SetActive(true);
            _itemsOptions.SetActive(true);
            _itemsGrid.SetActive(true);
        }
    }
}

