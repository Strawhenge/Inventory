using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Items
{
    public class ItemContainer
    {
        public ItemContainer(string name)
        {
            Name = name;
            CurrentItem = Maybe.None<InventoryItem>();
        }

        public event Action Changed;

        public string Name { get; }

        public Maybe<InventoryItem> CurrentItem { get; private set; }

        public bool IsCurrentItem(InventoryItem item) =>
            CurrentItem.HasSome(out var currentItem) && item == currentItem;

        internal void SetItem(InventoryItem item)
        {
            CurrentItem = Maybe.Some(item);
            Changed?.Invoke();
        }

        internal void UnsetItem()
        {
            CurrentItem = Maybe.None<InventoryItem>();
            Changed?.Invoke();
        }
    }
}