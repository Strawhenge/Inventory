using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        private readonly IHolsterComponent holster;
        private readonly IHandComponent hand;

        public SimpleDrawFromHolster(IHolsterComponent holster, IHandComponent hand)
        {
            this.holster = holster;
            this.hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            hand.SetItem(holster.TakeItem());
            endProcedure();
        }

        protected override void OnSkip()
        {
            hand.SetItem(holster.TakeItem());
        }
    }

}