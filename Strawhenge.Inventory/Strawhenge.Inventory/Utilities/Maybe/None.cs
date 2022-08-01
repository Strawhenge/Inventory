using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory
{
    internal sealed class None<T> : Maybe<T>
    {
        public override void Do(Action<T> action)
        {
        }

        public override bool HasSome(out T value)
        {
            value = default;
            return false;
        }

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new None<TNew>();

        public override T Reduce(Func<T> fallback) => fallback();

        public override IEnumerable<T> AsEnumerable() => Enumerable.Empty<T>();
    }
}
