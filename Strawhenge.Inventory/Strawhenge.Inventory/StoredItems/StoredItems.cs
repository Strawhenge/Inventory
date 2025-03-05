using System;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public class StoredItems : IStoredItems, IStoredItemsWeightCapacitySetter
    {
        readonly List<Item> _items = new List<Item>();

        public event Action<Item> ItemAdded;

        public event Action<Item> ItemRemoved;

        public int TotalItemsWeight { get; private set; }

        public int MaxItemsWeight { get; private set; }

        public IEnumerable<Item> Items => _items.ToArray();

        internal void Add(Item item, int weight)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Disappear;
            TotalItemsWeight += weight;
            ItemAdded?.Invoke(item);
        }

        internal void Remove(Item item, int weight)
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