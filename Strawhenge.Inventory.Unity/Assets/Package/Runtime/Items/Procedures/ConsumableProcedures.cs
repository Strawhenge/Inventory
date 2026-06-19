using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.ConsumableData;

namespace Strawhenge.Inventory.Unity.Items.Procedures
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

        public Procedure ConsumeLeftHand() => Consume(_data.AnimationSettings.ConsumeLeftHandId);

        public Procedure ConsumeRightHand() => Consume(_data.AnimationSettings.ConsumeRightHandId);

        Procedure Consume(Maybe<int> animationId)
        {
            return animationId
                .Map<Procedure>(trigger => new AnimatedConsumeItem(_consumeItemAnimationHandler, trigger))
                .Reduce(() => Procedure.Completed);
        }
    }
}