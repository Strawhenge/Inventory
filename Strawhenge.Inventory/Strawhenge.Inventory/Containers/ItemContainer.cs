using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public class ItemContainer : IItemContainer
    {
        public ItemContainer(string name)
        {
            Name = name;
            CurrentItem = Maybe.None<IItem>();
        }

        public event Action Changed;

        public string Name { get; }

        public Maybe<IItem> CurrentItem { get; private set; }

        public void SetItem(IItem item)
        {
            CurrentItem = Maybe.Some(item);
            Changed?.Invoke();
        }

        public void UnsetItem()
        {
            CurrentItem = Maybe.None<IItem>();
            Changed?.Invoke();
        }

        public bool IsCurrentItem(IItem item) =>
            CurrentItem.HasSome(out var currentItem) && item == currentItem;
    }
}