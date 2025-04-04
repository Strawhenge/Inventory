using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimplePutInHammerspace : Procedure
    {
        readonly HandScript _hand;

        public SimplePutInHammerspace(HandScript hand)
        {
            _hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            RemoveItemFromHand();
            endProcedure();
        }

        protected override void OnSkip() => RemoveItemFromHand();

        void RemoveItemFromHand()
        {
            _hand.TakeItem()
                .Do(x => x.Despawn());
        }
    }
}