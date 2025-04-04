using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimpleDrawFromHammerspace : Procedure
    {
        readonly IItemHelper _item;
        readonly HandScript _hand;

        public SimpleDrawFromHammerspace(IItemHelper item, HandScript hand)
        {
            _item = item;
            _hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            PlaceItemInHand();
            endProcedure();
        }

        protected override void OnSkip()
        {
            PlaceItemInHand();
        }

        void PlaceItemInHand()
        {
            _hand.SetItem(_item);
        }
    }
}