using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        readonly HolsterScript _holster;
        readonly HandScript _hand;

        public SimpleDrawFromHolster(HolsterScript holster, HandScript hand)
        {
            _holster = holster;
            _hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _holster.TakeItem().Do(_hand.SetItem);
            endProcedure();
        }

        protected override void OnSkip()
        {
            _holster.TakeItem().Do(_hand.SetItem);
        }
    }
}