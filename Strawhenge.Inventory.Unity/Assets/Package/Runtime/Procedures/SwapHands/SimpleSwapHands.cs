using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.SwapHands
{
    public class SimpleSwapHands : Procedure
    {
        private readonly IHandComponent sourceHand;
        private readonly IHandComponent destinationHand;

        public SimpleSwapHands(IHandComponent sourceHand, IHandComponent destinationHand)
        {
            this.sourceHand = sourceHand;
            this.destinationHand = destinationHand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Swap();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Swap();
        }

        void Swap()
        {
            destinationHand.SetItem(
                sourceHand.TakeItem());
        }
    }
}
