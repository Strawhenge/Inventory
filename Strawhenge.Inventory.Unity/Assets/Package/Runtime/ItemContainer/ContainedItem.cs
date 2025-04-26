using System;

namespace Strawhenge.Inventory.Unity
{
    public class ContainedItem<T>
    {
        readonly Action _onRemove;

        public ContainedItem(T item, Action onRemove = null)
        {
            _onRemove = onRemove ?? (() => { });

            Item = item;
        }

        public event Action Removed;

        public T Item { get; }

        public void RemoveFromContainer()
        {
            _onRemove();
            Removed?.Invoke();
        }
    }
}