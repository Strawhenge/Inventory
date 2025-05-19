using System;
using System.Collections.Generic;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Items
{
    public class StoredItems
    {
        readonly ILogger _logger;

        public StoredItems(ILogger logger)
        {
            _logger = logger;
        }

        readonly List<InventoryItem> _items = new List<InventoryItem>();

        public event Action<InventoryItem> ItemAdded;

        public event Action<InventoryItem> ItemRemoved;

        public int TotalItemsWeight { get; private set; }

        public int MaxItemsWeight { get; private set; }

        public IEnumerable<InventoryItem> Items => _items.ToArray();

        public void SetWeightCapacity(int maxWeight)
        {
            if (maxWeight < 0)
            {
                _logger.LogError($"'{nameof(maxWeight)}' cannot be negative.");
                return;
            }

            MaxItemsWeight = maxWeight;
        }

        internal void Add(InventoryItem item, int weight)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            TotalItemsWeight += weight;
            ItemAdded?.Invoke(item);
        }

        internal void Remove(InventoryItem item, int weight)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            TotalItemsWeight -= weight;
            ItemRemoved?.Invoke(item);
        }
    }
}