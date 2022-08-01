using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    internal sealed class Some<T> : Maybe<T>
    {
        private readonly T value;

        public Some(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.value = value;
        }

        public override bool HasSome(out T value)
        {
            value = this.value;
            return true;
        }

        public override void Do(Action<T> action) => action(value);

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new Some<TNew>(mapping(value));

        public override T Reduce(Func<T> fallback) => value;

        public override IEnumerable<T> AsEnumerable()
        {
            yield return value;
        }
    }
}
