using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class SimpleSwapHands : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHoldItemData _holdItemData;
        readonly HandScript _sourceHand;
        readonly HandScript _destinationHand;

        public SimpleSwapHands(
            ItemScriptInstance itemScriptInstance,
            IHoldItemData holdItemData,
            HandScript sourceHand,
            HandScript destinationHand)
        {
            _itemScriptInstance = itemScriptInstance;
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
            _destinationHand.SetItem(_itemScriptInstance, _holdItemData);
        }
    }
}