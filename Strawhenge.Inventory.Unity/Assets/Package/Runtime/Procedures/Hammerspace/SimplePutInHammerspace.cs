using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimplePutInHammerspace : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly HandScript _hand;

        public SimplePutInHammerspace(ItemScriptInstance itemScriptInstance, HandScript hand)
        {
            _itemScriptInstance = itemScriptInstance;
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
            _itemScriptInstance.Despawn();
        }
    }
}