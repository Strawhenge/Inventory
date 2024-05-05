using FunctionalUtilities;
using Strawhenge.Common.Factories;
using Strawhenge.Inventory.Apparel.Effects;

namespace Strawhenge.Inventory.Tests.UnitTests.ApparelEffects
{
    class HealthData : IEffectData
    {
        public int IncreaseAmount { get; set; }

        public Maybe<Effect> Create(IAbstractFactory abstractFactory) =>
            abstractFactory
                .Create<IEffectFactory<HealthData>>()
                .Create(this);
    }
}