using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimplePutInHammerspace : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly HandScript _hand;

        public SimplePutInHammerspace(ItemHelper itemHelper, HandScript hand)
        {
            _itemHelper = itemHelper;
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
            _hand.UnsetItem();
            _itemHelper.Despawn();
        }
    }
}