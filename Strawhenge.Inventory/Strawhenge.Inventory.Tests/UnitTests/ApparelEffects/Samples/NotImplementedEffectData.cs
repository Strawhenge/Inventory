using FunctionalUtilities;
using Strawhenge.Common.Factories;
using Strawhenge.Inventory.Apparel.Effects;

namespace Strawhenge.Inventory.Tests.UnitTests.ApparelEffects
{
    class NotImplementedEffectData : IEffectData
    {
        public Maybe<Effect> Create(IAbstractFactory abstractFactory) =>
            abstractFactory
                .TryCreate<IEffectFactory<NotImplementedEffectData>>()
                .Map(factory => factory.Create(this));
    }
}