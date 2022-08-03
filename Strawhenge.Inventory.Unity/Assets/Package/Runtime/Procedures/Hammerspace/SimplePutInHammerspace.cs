using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimplePutInHammerspace : Procedure
    {
        private readonly IHandComponent hand;

        public SimplePutInHammerspace(IHandComponent hand)
        {
            this.hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            RemoveItemFromHand();
            endProcedure();
        }

        protected override void OnSkip() => RemoveItemFromHand();

        private void RemoveItemFromHand()
        {
            var item = hand.TakeItem();
            item.Despawn();
        }
    }
}
