using FunctionalUtilities;
using Xunit;

namespace Strawhenge.Inventory.Tests._new
{
    static class MaybeExtensions
    {
        public static void VerifyIsNone<T>(this Maybe<T> maybe)
        {
            Assert.NotNull(maybe);
            Assert.False(maybe.HasSome());
        }

        public static void VerifyIsSome<T>(this Maybe<T> maybe, T expectedValue)
        {
            Assert.NotNull(maybe);
            Assert.True(maybe.HasSome(out var actualValue));
            Assert.Equal(expectedValue, actualValue);
        }
    }
}