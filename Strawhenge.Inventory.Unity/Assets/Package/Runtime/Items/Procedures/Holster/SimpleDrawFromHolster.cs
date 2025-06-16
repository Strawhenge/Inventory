using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class SimpleDrawFromHolster : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHoldItemData _holdItemData;
        readonly HolsterScript _holster;
        readonly HandScript _hand;

        public SimpleDrawFromHolster(
            ItemScriptInstance itemScriptInstance,
            IHoldItemData holdItemData,
            HolsterScript holster,
            HandScript hand)
        {
            _itemScriptInstance = itemScriptInstance;
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
            _hand.SetItem(_itemScriptInstance, _holdItemData);
        }
    }
}