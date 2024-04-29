using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public class StoredItems : IStoredItems
    {
        readonly List<IItem> _items = new List<IItem>();

        public IEnumerable<IItem> AllItems => _items.ToArray();

        public void Add(IItem item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Disappear;
            item.Dropped += Remove;
        }

        public void Remove(IItem item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Drop;
            item.Dropped -= Remove;
        }
    }
}