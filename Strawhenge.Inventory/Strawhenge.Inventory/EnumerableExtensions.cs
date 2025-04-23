using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory
{
    static class EnumerableExtensions
    {
        // TODO move to FunctionalUtilities
        public static IEnumerable<T> WhereSome<T>(this IEnumerable<Maybe<T>> enumerable)
        {
            foreach (var maybe in enumerable)
            {
                if (maybe.HasSome(out var value))
                    yield return value;
            }
        }
    }
}