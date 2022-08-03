using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedDrawFromHammerspace : Procedure
    {
        private readonly IProduceItemAnimationHandler animationHandler;
        private readonly IItemHelper item;
        private readonly IHandComponent hand;
        private readonly int animationId;

        private Action endProcedure = () => { };
        private bool itemInHand;
        private bool hasEnded;

        public AnimatedDrawFromHammerspace(
            IProduceItemAnimationHandler animationHandler,
            IItemHelper item,
            IHandComponent hand,
            int animationId)
        {
            this.animationHandler = animationHandler;
            this.item = item;
            this.hand = hand;
            this.animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            this.endProcedure = endProcedure;

            animationHandler.GrabItem += AnimationHandler_GrabItem;
            animationHandler.DrawEnded += AnimationHandler_DrawEnded;

            animationHandler.DrawItem(animationId);
        }

        protected override void OnSkip()
        {
            if (hasEnded) return;

            animationHandler.Interupt();
            AnimationHandler_DrawEnded();
        }

        private void AnimationHandler_DrawEnded()
        {
            hasEnded = true;
            animationHandler.GrabItem -= AnimationHandler_GrabItem;

            if (!itemInHand)
            {
                AnimationHandler_GrabItem();
            }

            endProcedure();
        }

        private void AnimationHandler_GrabItem()
        {
            animationHandler.GrabItem -= AnimationHandler_GrabItem;

            hand.SetItem(item);
            itemInHand = true;
        }
    }

}
