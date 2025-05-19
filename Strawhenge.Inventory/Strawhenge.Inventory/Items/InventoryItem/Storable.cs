using System;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Items
{
    public class Storable
    {
        readonly InventoryItem _item;
        readonly StoredItems _storedItems;

        public Storable(InventoryItem item, StoredItems storedItems, int weight)
        {
            _item = item;
            _storedItems = storedItems;
            Weight = weight;
        }

        public event Action Added;

        public event Action Removed;

        public int Weight { get; }

        public bool IsStored { get; private set; }

        public StoreItemResult AddToStorage()
        {
            if (IsStored)
                return StoreItemResult.Ok;

            if (HasInsufficientCapacity())
                return StoreItemResult.InsufficientCapacity;

            _storedItems.Add(_item, Weight);
            IsStored = true;
            Added?.Invoke();
            return StoreItemResult.Ok;
        }

        public void RemoveFromStorage()
        {
            Discard();
            _item.OnRemovedFromStorage();
        }

        internal void Discard()
        {
            _storedItems.Remove(_item, Weight);
            IsStored = false;
            Removed?.Invoke();
        }

        bool HasInsufficientCapacity() =>
            _storedItems.MaxItemsWeight - _storedItems.TotalItemsWeight < Weight;
    }
}