using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.ConsumeItem
{
    public class AnimatedConsumeItem : Procedure
    {
        readonly ConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly string _animationTrigger;
        Action _endProcedure;
        bool _isCompleted;

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

            _consumeItemAnimationHandler.Consumed += OnCompleted;
            _consumeItemAnimationHandler.Consume(_animationTrigger);
        }

        protected override void OnSkip()
        {
            if (_isCompleted) return;
            OnCompleted();
        }

        void OnCompleted()
        {
            if (_isCompleted) return;
            _isCompleted = true;
            _consumeItemAnimationHandler.Consumed -= OnCompleted;

            _endProcedure();
        }
    }
}