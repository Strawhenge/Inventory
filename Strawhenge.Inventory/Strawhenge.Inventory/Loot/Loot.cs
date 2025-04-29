using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Loot
{
    public class Loot<T>
    {
        readonly Action _onTake;

        public Loot(T item, Context context = null, Action onTake = null)
        {
            _onTake = onTake ?? (() =>
            {
            });

            Item = item;
            Context = context ?? Maybe.None<Context>();
        }

        public event Action Taken;

        public T Item { get; }

        public Maybe<Context> Context { get; }

        public T Take()
        {
            _onTake();
            Taken?.Invoke();

            return Item;
        }
    }
}