using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory
{
    static class DictionaryExtensions
    {
        // TODO move to FunctionalUtilities
        public static Maybe<TValue> MaybeGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out var value)
                ? Maybe.Some(value)
                : Maybe.None<TValue>();
    }
}