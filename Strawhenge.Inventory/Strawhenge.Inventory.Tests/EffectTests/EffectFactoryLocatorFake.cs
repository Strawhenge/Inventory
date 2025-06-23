using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Tests.EffectTests
{
    class EffectFactoryLocatorFake : IEffectFactoryLocator
    {
        readonly HealthFactory _healthFactory;

        public EffectFactoryLocatorFake(HealthFactory healthFactory)
        {
            _healthFactory = healthFactory;
        }

        public Maybe<IEffectFactory<T>> Find<T>()
        {
            if (_healthFactory is IEffectFactory<T> result)
                return Maybe.Some(result);

            return Maybe.None<IEffectFactory<T>>();
        }
    }
}