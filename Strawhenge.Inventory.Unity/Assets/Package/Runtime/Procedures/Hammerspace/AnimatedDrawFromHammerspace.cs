using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedDrawFromHammerspace : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly ItemHelper _item;
        readonly HandScript _hand;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHammerspace(
            IProduceItemAnimationHandler animationHandler,
            ItemHelper item,
            HandScript hand,
            int animationId)
        {
            _animationHandler = animationHandler;
            _item = item;
            _hand = hand;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.GrabItem += AnimationHandler_GrabItem;
            _animationHandler.DrawEnded += AnimationHandler_DrawEnded;

            _animationHandler.DrawItem(_animationId);
        }

        protected override void OnSkip()
        {
            if (_hasEnded) return;

            _animationHandler.Interupt();
            AnimationHandler_DrawEnded();
        }

        void AnimationHandler_DrawEnded()
        {
            _hasEnded = true;
            _animationHandler.GrabItem -= AnimationHandler_GrabItem;

            if (!_itemInHand)
            {
                AnimationHandler_GrabItem();
            }

            _endProcedure();
        }

        void AnimationHandler_GrabItem()
        {
            _animationHandler.GrabItem -= AnimationHandler_GrabItem;

            _hand.SetItem(_item);
            _itemInHand = true;
        }
    }

}
