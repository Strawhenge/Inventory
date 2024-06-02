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

        readonly Dictionary<IItem, StoredItemMenuEntryScript> _entries =
            new Dictionary<IItem, StoredItemMenuEntryScript>();

        IStoredItems _storedItems;

        internal void Set(IStoredItems storedItems)
        {
            _storedItems = storedItems;

            foreach (var item in _storedItems.Items)
                Add(item);

            _storedItems.ItemAdded += Add;
            _storedItems.ItemRemoved += Remove;
        }

        void Add(IItem item)
        {
            var entry = Instantiate(_menuEntryPrefab, parent: _entryContainer);
            entry.Set(item);
            _entries.Add(item, entry);

            UpdateWeightText();
        }

        void Remove(IItem item)
        {
            Destroy(_entries[item].gameObject);
            _entries.Remove(item);

            UpdateWeightText();
        }

        void UpdateWeightText() =>
            _weightText.text = $"{_storedItems.TotalItemsWeight} / {_storedItems.MaxItemsWeight}";
    }
}