using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Loot
{
    public class Loot<T>
    {
        readonly Action _onTake;

        public Loot(T content, Context context = null, Action onTake = null)
        {
            _onTake = onTake ?? (() =>
            {
            });

            Content = content;
            Context = context ?? Maybe.None<Context>();
        }

        public event Action Taken;

        public T Content { get; }

        public Maybe<Context> Context { get; }

        public T Take()
        {
            _onTake();
            Taken?.Invoke();

            return Content;
        }
    }
}