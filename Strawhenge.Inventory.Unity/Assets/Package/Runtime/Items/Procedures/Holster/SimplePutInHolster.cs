using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.HolsterItemData;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class SimplePutInHolster : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHolsterItemData _data;
        readonly HandScript _hand;
        readonly HolsterScript _holster;

        public SimplePutInHolster(
            ItemScriptInstance itemScriptInstance,
            IHolsterItemData data,
            HandScript hand,
            HolsterScript holster)
        {
            _itemScriptInstance = itemScriptInstance;
            _data = data;
            _hand = hand;
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            PutInHolster();
            endProcedure();
        }

        protected override void OnSkip()
        {
            PutInHolster();
        }

        void PutInHolster()
        {
            _hand.UnsetItem();
            _holster.SetItem(_itemScriptInstance, _data);
        }
    }
}