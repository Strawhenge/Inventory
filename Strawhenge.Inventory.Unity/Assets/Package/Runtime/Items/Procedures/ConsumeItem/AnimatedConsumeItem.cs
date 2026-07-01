using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class AnimatedConsumeItem : Procedure
    {
        readonly ConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _hasEnded;

        public AnimatedConsumeItem(
            ConsumeItemAnimationHandler consumeItemAnimationHandler,
            int animationId)
        {
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _consumeItemAnimationHandler.Consumed += End;
            _consumeItemAnimationHandler.Consume(_animationId);
        }

        protected override void OnSkip()
        {
            End();
            _consumeItemAnimationHandler.Interrupt();
        }

        void End()
        {
            if (_hasEnded) return;
            _hasEnded = true;

            _consumeItemAnimationHandler.Consumed -= End;
            _endProcedure();
        }
    }
}