using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimpleDrawFromHammerspace : Procedure
    {
        private readonly IItemHelper item;
        private readonly IHandComponent hand;

        public SimpleDrawFromHammerspace(IItemHelper item, IHandComponent hand)
        {
            this.item = item;
            this.hand = hand;
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

        private void PlaceItemInHand()
        {
            hand.SetItem(item);
        }
    }
}