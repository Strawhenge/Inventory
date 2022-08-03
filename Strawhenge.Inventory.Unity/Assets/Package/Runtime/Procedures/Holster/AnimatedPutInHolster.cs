using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedPutInHolster : Procedure
    {
        private readonly IProduceItemAnimationHandler animationHandler;
        private readonly IHandComponent hand;
        private readonly IHolsterComponent holster;
        private readonly int animationId;

        private Action endProcedure;
        private bool itemInHolster;
        private bool hasEnded;

        public AnimatedPutInHolster(IProduceItemAnimationHandler animationHandler, IHandComponent hand, IHolsterComponent holster, int animationId)
        {
            this.animationHandler = animationHandler;
            this.hand = hand;
            this.holster = holster;
            this.animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            this.endProcedure = endProcedure;

            animationHandler.ReleaseItem += PutItemInHolster;
            animationHandler.PutAwayEnded += End;
            animationHandler.PutAwayItem(animationId);
        }

        protected override void OnSkip()
        {
            if (hasEnded) return;

            animationHandler.Interupt();
            End();
        }

        private void PutItemInHolster()
        {
            itemInHolster = true;
            animationHandler.ReleaseItem -= PutItemInHolster;
            var item = hand.TakeItem();
            holster.SetItem(item);
        }

        private void End()
        {
            hasEnded = true;
            animationHandler.PutAwayEnded -= End;

            if (!itemInHolster) PutItemInHolster();

            endProcedure();
        }
    }
}