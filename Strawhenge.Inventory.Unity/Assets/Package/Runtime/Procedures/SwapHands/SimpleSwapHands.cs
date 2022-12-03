using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.SwapHands
{
    public class SimpleSwapHands : Procedure
    {
        readonly IHandComponent _sourceHand;
        readonly IHandComponent _destinationHand;

        public SimpleSwapHands(IHandComponent sourceHand, IHandComponent destinationHand)
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
            _destinationHand.SetItem(
                _sourceHand.TakeItem());
        }
    }
}
