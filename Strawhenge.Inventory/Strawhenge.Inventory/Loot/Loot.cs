using System;

namespace Strawhenge.Inventory.Loot
{
    public class Loot<T>
    {
        readonly Action _onTake;

        public Loot(T item, Action onTake = null)
        {
            _onTake = onTake ?? (() => { });

            Item = item;
        }

        public event Action Taken;

        public T Item { get; }

        public T Take()
        {
            _onTake();
            Taken?.Invoke();

            return Item;
        }
    }
}