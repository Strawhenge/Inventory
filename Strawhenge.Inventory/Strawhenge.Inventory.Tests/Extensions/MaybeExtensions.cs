using System.Collections.Generic;
using FunctionalUtilities;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    static class MaybeExtensions
    {
        public static void VerifyIsNone<T>(this Maybe<T> maybe)
        {
            Assert.NotNull(maybe);

            if (maybe.HasSome(out var actualValue))
                Assert.Fail($"Expected None of '{typeof(T).Name}', but was Some({Describe(actualValue)}).");
        }

        public static T VerifyIsSome<T>(this Maybe<T> maybe)
        {
            Assert.NotNull(maybe);

            if (!maybe.HasSome(out var actualValue))
                Assert.Fail($"Expected Some of '{typeof(T).Name}', but was None.");

            return actualValue;
        }

        public static T VerifyIsSome<T>(this Maybe<T> maybe, T expectedValue)
        {
            Assert.NotNull(maybe);

            if (!maybe.HasSome(out var actualValue))
                Assert.Fail($"Expected Some({Describe(expectedValue)}) of '{typeof(T).Name}', but was None.");

            if (!EqualityComparer<T>.Default.Equals(expectedValue, actualValue))
                Assert.Fail(
                    $"Expected Some({Describe(expectedValue)}) of '{typeof(T).Name}', " +
                    $"but was Some({Describe(actualValue)}){DifferentInstanceNote(expectedValue, actualValue)}.");

            return actualValue;
        }

        static string Describe(object value) => value == null ? "null" : $"'{value}'";

        static string DifferentInstanceNote(object expected, object actual) =>
            Describe(expected) == Describe(actual) ? ", a different instance" : string.Empty;
    }
}
