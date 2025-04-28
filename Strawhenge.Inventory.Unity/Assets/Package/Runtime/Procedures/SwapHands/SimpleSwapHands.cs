using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.SwapHands
{
    public class SimpleSwapHands : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IHoldItemData _holdItemData;
        readonly HandScript _sourceHand;
        readonly HandScript _destinationHand;

        public SimpleSwapHands(
            ItemHelper itemHelper,
            IHoldItemData holdItemData,
            HandScript sourceHand,
            HandScript destinationHand)
        {
            _itemHelper = itemHelper;
            _holdItemData = holdItemData;
            _sourceHand = sourceHand;
            _destinationHand = destinationHand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Swap();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Swap();
        }

        void Swap()
        {
            _sourceHand.UnsetItem();
            _destinationHand.SetItem(_itemHelper, _holdItemData);
        }
    }
}