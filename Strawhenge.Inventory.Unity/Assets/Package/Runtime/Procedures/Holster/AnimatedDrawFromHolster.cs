using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedDrawFromHolster : Procedure
    {
        private readonly IProduceItemAnimationHandler animationHandler;
        private readonly IHolsterComponent holster;
        private readonly IHandComponent hand;
        private readonly int animationId;

        private Action endProcedure = () => { };
        private bool itemInHand;
        private bool hasEnded;

        public AnimatedDrawFromHolster(IProduceItemAnimationHandler animationHandler, IHolsterComponent holster, IHandComponent hand, int animationId)
        {
            this.animationHandler = animationHandler;
            this.holster = holster;
            this.hand = hand;
            this.animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            this.endProcedure = endProcedure;

            animationHandler.GrabItem += PutItemInHand;
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
            animationHandler.DrawEnded -= AnimationHandler_DrawEnded;

            if (!itemInHand) PutItemInHand();

            endProcedure();
        }

        private void PutItemInHand()
        {
            animationHandler.GrabItem -= PutItemInHand;
            hand.SetItem(holster.TakeItem());
            itemInHand = true;
        }
    }
}