using FunctionalUtilities;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    public static class AssertMaybe
    {
        public static void IsNone<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);

            var hasValue = HasValue(maybe);
            Assert.False(hasValue);
        }

        public static void IsSome<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);

            var hasValue = HasValue(maybe);
            Assert.True(hasValue);
        }

        public static void IsSome<T>(Maybe<T> maybe, T expectedValue)
        {
            IsSome(maybe);
            Assert.Equal(expectedValue, (T)maybe);
        }

        static bool HasValue<T>(Maybe<T> maybe) => maybe
            .Map(_ => true)
            .Reduce(() => false);
    }
}