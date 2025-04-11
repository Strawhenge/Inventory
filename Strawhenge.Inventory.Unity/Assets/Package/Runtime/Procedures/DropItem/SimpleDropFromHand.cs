using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHand : Procedure
    {
        readonly HandScript _hand;

        public SimpleDropFromHand(HandScript hand)
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
            _hand.TakeItem().Do(
                x => x.Release());
        }
    }
}