using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        readonly HandComponent _hand;
        readonly HolsterComponent _holster;

        public SimplePutInHolster(HandComponent hand, HolsterComponent holster)
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

