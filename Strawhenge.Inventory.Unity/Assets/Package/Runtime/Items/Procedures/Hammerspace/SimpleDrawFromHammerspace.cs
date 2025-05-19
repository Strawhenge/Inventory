using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class SimpleDrawFromHammerspace : Procedure
    {
        readonly ItemScriptInstance _item;
        readonly IHoldItemData _holdItemData;
        readonly HandScript _hand;

        public SimpleDrawFromHammerspace(
            ItemScriptInstance item,
            IHoldItemData holdItemData,
            HandScript hand)
        {
            _item = item;
            _holdItemData = holdItemData;
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
            _hand.SetItem(_item, _holdItemData);
        }
    }
}