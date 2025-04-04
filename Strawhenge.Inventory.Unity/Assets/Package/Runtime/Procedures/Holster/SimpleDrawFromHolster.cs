using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        readonly HolsterComponent _holster;
        readonly HandComponent _hand;

        public SimpleDrawFromHolster(HolsterComponent holster, HandComponent hand)
        {
            _holster = holster;
            _hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _hand.SetItem(_holster.TakeItem());
            endProcedure();
        }

        protected override void OnSkip()
        {
            _hand.SetItem(_holster.TakeItem());
        }
    }

}