using System;

namespace Strawhenge.Inventory.Items.Storables
{
    public class Storable
    {
        readonly Item _item;
        readonly StoredItems _storedItems;

        public Storable(Item item, StoredItems storedItems, int weight)
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
            _storedItems.Remove(_item, Weight);
            IsStored = false;
            Removed?.Invoke();
            _item.OnRemovedFromStorage();
        }

        bool HasInsufficientCapacity() =>
            _storedItems.MaxItemsWeight - _storedItems.TotalItemsWeight < Weight;
    }
}