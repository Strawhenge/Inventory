using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Menu.Storage
{
    public class StorageMenuScript : MonoBehaviour
    {
        [FormerlySerializedAs("_menuEntryPrefab")] [SerializeField]
        StoredItemMenuEntryScript _menuEntryScriptPrefab;

        [SerializeField] RectTransform _entryContainer;

        readonly Dictionary<IItem, StoredItemMenuEntryScript> _entries =
            new Dictionary<IItem, StoredItemMenuEntryScript>();

        internal void Set(IStoredItems storedItems)
        {
            foreach (var item in storedItems.Items)
                Add(item);

            storedItems.ItemAdded += Add;
            storedItems.ItemRemoved += Remove;
        }

        void Add(IItem item)
        {
            var entry = Instantiate(_menuEntryScriptPrefab, parent: _entryContainer);
            entry.Set(item);
            _entries.Add(item, entry);
        }

        void Remove(IItem item)
        {
            Destroy(_entries[item].gameObject);
            _entries.Remove(item);
        }
    }
}