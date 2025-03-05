using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Storage
{
    public class StorageMenuScript : MonoBehaviour
    {
        [SerializeField] Text _weightText;
        [SerializeField] StoredItemMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        readonly Dictionary<Item, StoredItemMenuEntryScript> _entries =
            new Dictionary<Item, StoredItemMenuEntryScript>();

        IStoredItems _storedItems;

        internal void Set(IStoredItems storedItems)
        {
            _storedItems = storedItems;

            foreach (var item in _storedItems.Items)
                Add(item);

            _storedItems.ItemAdded += Add;
            _storedItems.ItemRemoved += Remove;
        }

        void Add(Item item)
        {
            var entry = Instantiate(_menuEntryPrefab, parent: _entryContainer);
            entry.Set(item);
            _entries.Add(item, entry);

            UpdateWeightText();
        }

        void Remove(Item item)
        {
            Destroy(_entries[item].gameObject);
            _entries.Remove(item);

            UpdateWeightText();
        }

        void UpdateWeightText() =>
            _weightText.text = $"{_storedItems.TotalItemsWeight} / {_storedItems.MaxItemsWeight}";
    }
}