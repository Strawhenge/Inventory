using System;

namespace Strawhenge.Inventory.Unity
{
    public interface IContainedItem<T>
    {
        event Action Removed;

        T Item { get; }

        void RemoveFromContainer();
    }
}