using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        readonly IHolsterComponent _holster;
        readonly IHandComponent _hand;

        public SimpleDrawFromHolster(IHolsterComponent holster, IHandComponent hand)
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