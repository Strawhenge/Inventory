using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public class ItemContainer : IItemContainer
    {
        public ItemContainer(string name)
        {
            Name = name;
            CurrentItem = Maybe.None<Item>();
        }

        public event Action Changed;

        public string Name { get; }

        public Maybe<Item> CurrentItem { get; private set; }

        public void SetItem(Item item)
        {
            CurrentItem = Maybe.Some(item);
            Changed?.Invoke();
        }

        public void UnsetItem()
        {
            CurrentItem = Maybe.None<Item>();
            Changed?.Invoke();
        }

        public bool IsCurrentItem(Item item) =>
            CurrentItem.HasSome(out var currentItem) && item == currentItem;
    }
}