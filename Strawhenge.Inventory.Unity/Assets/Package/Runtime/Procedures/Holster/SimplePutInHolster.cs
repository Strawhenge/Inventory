using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        private readonly IHandComponent hand;
        private readonly IHolsterComponent holster;

        public SimplePutInHolster(IHandComponent hand, IHolsterComponent holster)
        {
            this.hand = hand;
            this.holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            var item = hand.TakeItem();
            holster.SetItem(item);
            endProcedure();
        }

        protected override void OnSkip()
        {
            var item = hand.TakeItem();
            holster.SetItem(item);
        }
    }
}

