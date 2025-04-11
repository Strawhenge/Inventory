using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        readonly HandScript _hand;
        readonly HolsterScript _holster;

        public SimplePutInHolster(HandScript hand, HolsterScript holster)
        {
            _hand = hand;
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _hand.TakeItem().Do(
                item => _holster.SetItem(item));

            endProcedure();
        }

        protected override void OnSkip()
        {
            _hand.TakeItem().Do(
                item => _holster.SetItem(item));
        }
    }
}