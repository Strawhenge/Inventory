using Moq;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Tests
{
    public static class MockExtensions
    {
        public static void VerifyOnce<T>(this Mock<T> mock, Expression<Action<T>> expression) where T : class
        {
            mock.Verify(expression, Times.Once);
        }

        public static IReturnsResult<T> SetupGetNone<T, TMaybe>(this Mock<T> mock, Expression<Func<T, Maybe<TMaybe>>> expression) where T : class
        {
            return mock.SetupGet(expression)
                .Returns(() => Maybe.None<TMaybe>());
        }

        public static IReturnsResult<T> SetupGetSome<T, TMaybe>(this Mock<T> mock, Expression<Func<T, Maybe<TMaybe>>> expression, TMaybe value) where T : class
        {
            return mock.SetupGet(expression).Returns(value);
        }

        public static IReturnsResult<T> SetupNone<T, TMaybe>(this Mock<T> mock, Expression<Func<T, Maybe<TMaybe>>> expression) where T : class
        {
            return mock.Setup(expression)
                .Returns(() => Maybe.None<TMaybe>());
        }

        public static IReturnsResult<T> SetupSome<T, TMaybe>(this Mock<T> mock, Expression<Func<T, Maybe<TMaybe>>> expression, TMaybe value) where T : class
        {
            return mock.Setup(expression).Returns(value);
        }
    }
}
