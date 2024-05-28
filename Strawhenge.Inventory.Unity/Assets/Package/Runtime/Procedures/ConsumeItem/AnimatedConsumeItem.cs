using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.ConsumeItem
{
    public class AnimatedConsumeItem : Procedure
    {
        readonly IConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly int _animationId;
        readonly bool _invert;
        Action _endProcedure;
        bool _isCompleted;

        public AnimatedConsumeItem(
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            int animationId,
            bool invert)
        {
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _animationId = animationId;
            _invert = invert;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _consumeItemAnimationHandler.Consumed += OnCompleted;
            _consumeItemAnimationHandler.Consume(_animationId, _invert);
        }

        protected override void OnSkip()
        {
            if (_isCompleted) return;

            _consumeItemAnimationHandler.Interrupt();
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