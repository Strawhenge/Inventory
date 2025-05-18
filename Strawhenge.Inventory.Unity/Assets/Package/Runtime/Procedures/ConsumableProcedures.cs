using FunctionalUtilities;
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

        public Procedure ConsumeLeftHand() => Consume(_data.AnimationSettings.ConsumeLeftHandTrigger);

        public Procedure ConsumeRightHand() => Consume(_data.AnimationSettings.ConsumeRightHandTrigger);

        Procedure Consume(Maybe<string> animationTrigger)
        {
            return animationTrigger
                .Map<Procedure>(trigger => new AnimatedConsumeItem(_consumeItemAnimationHandler, trigger))
                .Reduce(() => Procedure.Completed);
        }
    }
}