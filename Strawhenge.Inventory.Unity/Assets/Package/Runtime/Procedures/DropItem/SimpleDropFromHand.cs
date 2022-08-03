using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHand : Procedure
    {
        private readonly IHandComponent hand;

        public SimpleDropFromHand(IHandComponent hand)
        {
            this.hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Drop();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Drop();
        }

        private void Drop()
        {
            hand.TakeItem().Release();
        }
    }
}
