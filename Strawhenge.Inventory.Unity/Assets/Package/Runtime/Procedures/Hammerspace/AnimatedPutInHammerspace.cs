using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedPutInHammerspace : Procedure
    {
        private readonly IProduceItemAnimationHandler animationHandler;
        private readonly IHandComponent hand;
        private readonly int animationId;

        private Action endProcedure;
        private bool itemIsPutAway;
        private bool hasEnded;

        public AnimatedPutInHammerspace(IProduceItemAnimationHandler animationHandler, IHandComponent hand, int animationId)
        {
            this.animationHandler = animationHandler;
            this.hand = hand;
            this.animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            this.endProcedure = endProcedure;

            animationHandler.ReleaseItem += AnimationHandler_ReleaseItem;
            animationHandler.PutAwayEnded += AnimationHandler_PutAwayEnded;
            animationHandler.PutAwayItem(animationId);
        }

        protected override void OnSkip()
        {
            if (hasEnded) return;

            animationHandler.Interupt();
            AnimationHandler_PutAwayEnded();
        }

        private void AnimationHandler_PutAwayEnded()
        {
            hasEnded = true;
            animationHandler.PutAwayEnded -= AnimationHandler_PutAwayEnded;
            if (!itemIsPutAway) AnimationHandler_ReleaseItem();

            endProcedure();
        }

        private void AnimationHandler_ReleaseItem()
        {
            animationHandler.ReleaseItem -= AnimationHandler_ReleaseItem;
            itemIsPutAway = true;
            var item = hand.TakeItem();
            item.Despawn();
        }
    }

}