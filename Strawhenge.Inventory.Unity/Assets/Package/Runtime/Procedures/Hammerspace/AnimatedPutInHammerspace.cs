using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedPutInHammerspace : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly HandScript _hand;
        readonly string _animationTrigger;

        Action _endProcedure;
        bool _itemIsPutAway;
        bool _hasEnded;

        public AnimatedPutInHammerspace(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            HandScript hand,
            string animationTrigger)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _hand = hand;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += AnimationHandler_ReleaseItem;
            _animationHandler.PutAwayEnded += AnimationHandler_PutAwayEnded;
            _animationHandler.PutAwayItem(_animationTrigger);
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
            _itemScriptInstance.Despawn();
        }
    }

}