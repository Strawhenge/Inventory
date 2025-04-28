using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimpleDrawFromHolster : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IHoldItemData _holdItemData;
        readonly HolsterScript _holster;
        readonly HandScript _hand;

        public SimpleDrawFromHolster(
            ItemHelper itemHelper,
            IHoldItemData holdItemData,
            HolsterScript holster,
            HandScript hand)
        {
            _itemHelper = itemHelper;
            _holdItemData = holdItemData;
            _holster = holster;
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
            _holster.UnsetItem();
            _hand.SetItem(_itemHelper, _holdItemData);
        }
    }
}