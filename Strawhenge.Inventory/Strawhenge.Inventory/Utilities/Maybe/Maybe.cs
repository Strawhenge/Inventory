using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Some<T>(value);

        public static Maybe<T> None<T>() => new None<T>();
    }

    public abstract partial class Maybe<T>
    {
        public static implicit operator Maybe<T>(T value) => Maybe.Some(value);

        public static explicit operator T(Maybe<T> maybe) => maybe.Reduce(
            () => throw new InvalidCastException($"Cannot convert None to {typeof(T)}"));

        public abstract bool HasSome(out T value);

        public abstract void Do(Action<T> action);

        public abstract Maybe<TNew> Map<TNew>(Func<T, TNew> mapping);

        public abstract T Reduce(Func<T> reducer);

        public abstract IEnumerable<T> AsEnumerable();
    }
}
