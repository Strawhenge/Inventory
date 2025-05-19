using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class AnimatedConsumeItem : Procedure
    {
        readonly ConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly string _animationTrigger;

        Action _endProcedure = () => { };
        bool _hasEnded;

        public AnimatedConsumeItem(
            ConsumeItemAnimationHandler consumeItemAnimationHandler,
            string animationTrigger)
        {
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _consumeItemAnimationHandler.Consumed += End;
            _consumeItemAnimationHandler.Consume(_animationTrigger);
        }

        protected override void OnSkip()
        {
            End();
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