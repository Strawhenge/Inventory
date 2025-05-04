using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class StoredItemsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemMenuScript _itemMenuScript;
        [SerializeField] StoredItemsMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _menuEntriesParent;

        readonly Dictionary<Item, StoredItemsMenuEntryScript> _menuEntriesByItem = new();

        Item _selectedItem;

        public void SetInventory(Inventory inventory)
        {
            inventory.StoredItems.ItemAdded += item =>
            {
                _selectedItem = item;

                var menuEntry = Instantiate(_menuEntryPrefab, _menuEntriesParent);
                menuEntry.SetItem(item);
                menuEntry.Selected += () => _itemMenuScript.SetItem(item);

                _menuEntriesByItem.Add(item, menuEntry);
            };

            inventory.StoredItems.ItemRemoved += item =>
            {
                var menuEntry = _menuEntriesByItem[item];
                _menuEntriesByItem.Remove(item);
                
                Destroy(menuEntry.gameObject);

                if (_selectedItem == item)
                    _itemMenuScript.UnsetItem();
            };
        }
    }
}