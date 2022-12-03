using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedPutInHolster : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly IHandComponent _hand;
        readonly IHolsterComponent _holster;
        readonly int _animationId;

        Action _endProcedure;
        bool _itemInHolster;
        bool _hasEnded;

        public AnimatedPutInHolster(IProduceItemAnimationHandler animationHandler, IHandComponent hand, IHolsterComponent holster, int animationId)
        {
            _animationHandler = animationHandler;
            _hand = hand;
            _holster = holster;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += PutItemInHolster;
            _animationHandler.PutAwayEnded += End;
            _animationHandler.PutAwayItem(_animationId);
        }

        protected override void OnSkip()
        {
            if (_hasEnded) return;

            _animationHandler.Interupt();
            End();
        }

        void PutItemInHolster()
        {
            _itemInHolster = true;
            _animationHandler.ReleaseItem -= PutItemInHolster;
            var item = _hand.TakeItem();
            _holster.SetItem(item);
        }

        void End()
        {
            _hasEnded = true;
            _animationHandler.PutAwayEnded -= End;

            if (!_itemInHolster) PutItemInHolster();

            _endProcedure();
        }
    }
}