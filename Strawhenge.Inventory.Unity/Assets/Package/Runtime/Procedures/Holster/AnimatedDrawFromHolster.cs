using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedDrawFromHolster : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly HolsterComponent _holster;
        readonly HandComponent _hand;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHolster(IProduceItemAnimationHandler animationHandler, HolsterComponent holster, HandComponent hand, int animationId)
        {
            _animationHandler = animationHandler;
            _holster = holster;
            _hand = hand;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.GrabItem += PutItemInHand;
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
            _animationHandler.DrawEnded -= AnimationHandler_DrawEnded;

            if (!_itemInHand) PutItemInHand();

            _endProcedure();
        }

        void PutItemInHand()
        {
            _animationHandler.GrabItem -= PutItemInHand;
            _hand.SetItem(_holster.TakeItem());
            _itemInHand = true;
        }
    }
}