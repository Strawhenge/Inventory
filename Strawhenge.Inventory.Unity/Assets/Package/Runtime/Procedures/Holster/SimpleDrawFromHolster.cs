using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        readonly HolsterScript _holster;
        readonly HandComponent _hand;

        public SimpleDrawFromHolster(HolsterScript holster, HandComponent hand)
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