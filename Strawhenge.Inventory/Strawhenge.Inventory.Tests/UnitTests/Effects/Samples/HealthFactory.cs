using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Tests.UnitTests.Effects
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