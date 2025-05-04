using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class StoredItemsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemMenuScript _itemMenuScript;
        [SerializeField] StoredItemsMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _menuEntriesParent;
        [SerializeField] Text _capacityText;

        readonly Dictionary<Item, StoredItemsMenuEntryScript> _menuEntriesByItem = new();

        StoredItems _storedItems;
        Item _selectedItem;

        internal void SetInventory(Inventory inventory)
        {
            _storedItems = inventory.StoredItems;
            _storedItems.ItemAdded += item =>
            {
                var menuEntry = Instantiate(_menuEntryPrefab, _menuEntriesParent);
                menuEntry.SetItem(item);
                menuEntry.Selected += () =>
                {
                    _itemMenuScript.SetItem(item);
                    _selectedItem = item;
                };

                _menuEntriesByItem.Add(item, menuEntry);

                UpdateCapacity();
            };

            _storedItems.ItemRemoved += item =>
            {
                var menuEntry = _menuEntriesByItem[item];
                _menuEntriesByItem.Remove(item);

                Destroy(menuEntry.gameObject);

                if (_selectedItem == item)
                {
                    _selectedItem = null;
                    _itemMenuScript.UnsetItem();
                }

                UpdateCapacity();
            };

            UpdateCapacity();
        }

        void UpdateCapacity() =>
            _capacityText.text = $"{_storedItems.TotalItemsWeight} / {_storedItems.MaxItemsWeight}";
    }
}