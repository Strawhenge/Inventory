using System;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public class StoredItems : IStoredItems, IStoredItemsWeightCapacitySetter
    {
        readonly List<IItem> _items = new List<IItem>();

        public event Action<IItem> ItemAdded;

        public event Action<IItem> ItemRemoved;

        public int TotalItemsWeight { get; private set; }

        public int MaxItemsWeight { get; private set; }

        public IEnumerable<IItem> Items => _items.ToArray();

        internal void Add(IItem item, int weight)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Disappear;
            TotalItemsWeight += weight;
            ItemAdded?.Invoke(item);
        }

        internal void Remove(IItem item, int weight)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Drop;
            TotalItemsWeight -= weight;
            ItemRemoved?.Invoke(item);
        }

        void IStoredItemsWeightCapacitySetter.SetWeightCapacity(int maxWeight) => MaxItemsWeight = maxWeight;
    }
}