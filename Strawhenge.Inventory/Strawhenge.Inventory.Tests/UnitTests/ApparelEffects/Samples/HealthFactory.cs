using Strawhenge.Inventory.Apparel.Effects;

namespace Strawhenge.Inventory.Tests.UnitTests.ApparelEffects
{
    class HealthFactory : IEffectFactory<HealthData>
    {
        readonly Health _health;

        public HealthFactory(Health health)
        {
            _health = health;
        }

        public Effect Create(HealthData data) => new IncreaseHealthEffect(_health, data.IncreaseAmount);
    }
}