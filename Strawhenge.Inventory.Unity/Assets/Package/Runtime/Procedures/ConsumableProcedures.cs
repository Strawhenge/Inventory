using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Consumables;
using Strawhenge.Inventory.Unity.Procedures.ConsumeItem;

namespace Strawhenge.Inventory.Unity.Procedures
{
    class ConsumableProcedures : IConsumableProcedures
    {
        readonly IConsumableData _data;
        readonly ConsumeItemAnimationHandler _consumeItemAnimationHandler;

        public ConsumableProcedures(
            IConsumableData data,
            ConsumeItemAnimationHandler consumeItemAnimationHandler)
        {
            _data = data;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
        }

        public Procedure ConsumeLeftHand() => Consume(_data.AnimationId, true);

        public Procedure ConsumeRightHand() => Consume(_data.AnimationId, false);

        Procedure Consume(int animationId, bool inverted)
        {
            if (animationId == 0)
                return Procedure.Completed;

            return new AnimatedConsumeItem(_consumeItemAnimationHandler, animationId, inverted);
        }
    }
}