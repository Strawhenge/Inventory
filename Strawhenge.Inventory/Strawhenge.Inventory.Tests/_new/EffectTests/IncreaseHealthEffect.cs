using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Tests._new.EffectTests
{
    class IncreaseHealthEffect : Effect
    {
        readonly Health _health;
        readonly int _increaseAmount;

        public IncreaseHealthEffect(Health health, int increaseAmount)
        {
            _health = health;
            _increaseAmount = increaseAmount;
        }

        protected override void PerformApply()
        {
            _health.Amount += _increaseAmount;
        }

        protected override void PerformRevert()
        {
            _health.Amount -= _increaseAmount;
        }
    }
}