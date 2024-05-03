using System;

namespace Strawhenge.Inventory.Unity
{
    public class ContainedItem<T> : IContainedItem<T>
    {
        readonly Action _removeStrategy;

        public ContainedItem(T item, Action removeStrategy)
        {
            _removeStrategy = removeStrategy;
            Item = item;
        }

        public event Action Removed;

        public T Item { get; }

        public void RemoveFromContainer()
        {
            _removeStrategy();
            Removed?.Invoke();
        }
    }
}