using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        readonly IHandComponent _hand;
        readonly IHolsterComponent _holster;

        public SimplePutInHolster(IHandComponent hand, IHolsterComponent holster)
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

