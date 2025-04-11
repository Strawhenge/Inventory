using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.SwapHands
{
    public class SimpleSwapHands : Procedure
    {
        readonly HandScript _sourceHand;
        readonly HandScript _destinationHand;

        public SimpleSwapHands(HandScript sourceHand, HandScript destinationHand)
        {
            _sourceHand = sourceHand;
            _destinationHand = destinationHand;
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
            _sourceHand.TakeItem()
                .Do(_destinationHand.SetItem);
        }
    }
}