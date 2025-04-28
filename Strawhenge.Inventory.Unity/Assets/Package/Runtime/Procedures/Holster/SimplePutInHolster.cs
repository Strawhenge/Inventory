using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class SimplePutInHolster : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IHolsterItemData _data;
        readonly HandScript _hand;
        readonly HolsterScript _holster;

        public SimplePutInHolster(
            ItemHelper itemHelper,
            IHolsterItemData data,
            HandScript hand,
            HolsterScript holster)
        {
            _itemHelper = itemHelper;
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
            _holster.SetItem(_itemHelper, _data);
        }
    }
}