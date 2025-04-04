using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHand : Procedure
    {
        readonly HandComponent _hand;

        public SimpleDropFromHand(HandComponent hand)
        {
            _hand = hand;
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

        void Drop()
        {
            _hand.TakeItem().Release();
        }
    }
}
