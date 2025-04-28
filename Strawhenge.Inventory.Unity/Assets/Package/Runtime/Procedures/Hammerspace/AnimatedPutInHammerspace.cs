using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedPutInHammerspace : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly ItemHelper _itemHelper;
        readonly HandScript _hand;
        readonly int _animationId;

        Action _endProcedure;
        bool _itemIsPutAway;
        bool _hasEnded;

        public AnimatedPutInHammerspace(
            IProduceItemAnimationHandler animationHandler,
            ItemHelper itemHelper,
            HandScript hand,
            int animationId)
        {
            _animationHandler = animationHandler;
            _itemHelper = itemHelper;
            _hand = hand;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += AnimationHandler_ReleaseItem;
            _animationHandler.PutAwayEnded += AnimationHandler_PutAwayEnded;
            _animationHandler.PutAwayItem(_animationId);
        }

        protected override void OnSkip()
        {
            if (_hasEnded) return;

            _animationHandler.Interupt();
            AnimationHandler_PutAwayEnded();
        }

        void AnimationHandler_PutAwayEnded()
        {
            _hasEnded = true;
            _animationHandler.PutAwayEnded -= AnimationHandler_PutAwayEnded;
            if (!_itemIsPutAway) AnimationHandler_ReleaseItem();

            _endProcedure();
        }

        void AnimationHandler_ReleaseItem()
        {
            _animationHandler.ReleaseItem -= AnimationHandler_ReleaseItem;
            _itemIsPutAway = true;

            _hand.UnsetItem();
            _itemHelper.Despawn();
        }
    }

}