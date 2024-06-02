using System;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public class StoredItems : IStoredItems
    {
        readonly List<IItem> _items = new List<IItem>();

        public event Action<IItem> ItemAdded;

        public event Action<IItem> ItemRemoved;

        public IEnumerable<IItem> Items => _items.ToArray();

        public void Add(IItem item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Disappear;
            ItemAdded?.Invoke(item);
        }

        public void Remove(IItem item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Drop;
            ItemRemoved?.Invoke(item);
        }
    }
}