using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        readonly HandComponent _hand;
        readonly HolsterScript _holster;

        public SimplePutInHolster(HandComponent hand, HolsterScript holster)
        {
            _hand = hand;
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            var item = _hand.TakeItem();
            _holster.SetItem(item);
            endProcedure();
        }

        protected override void OnSkip()
        {
            var item = _hand.TakeItem();
            _holster.SetItem(item);
        }
    }
}

