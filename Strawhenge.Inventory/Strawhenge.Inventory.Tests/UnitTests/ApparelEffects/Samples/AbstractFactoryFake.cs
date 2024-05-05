using System;
using FunctionalUtilities;
using Strawhenge.Common.Factories;

namespace Strawhenge.Inventory.Tests.UnitTests.ApparelEffects
{
    class AbstractFactoryFake : IAbstractFactory
    {
        readonly HealthFactory _healthFactory;

        public AbstractFactoryFake(HealthFactory healthFactory)
        {
            _healthFactory = healthFactory;
        }

        public T Create<T>() => TryCreate<T>().Reduce(() => throw new InvalidOperationException());

        public Maybe<T> TryCreate<T>()
        {
            if (_healthFactory is T result)
                return result;

            return Maybe.None<T>();
        }
    }
}