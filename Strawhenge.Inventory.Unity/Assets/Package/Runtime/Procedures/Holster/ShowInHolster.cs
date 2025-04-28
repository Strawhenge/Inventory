using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class ShowInHolster : Procedure
    {
        readonly ItemScriptInstance _item;
        readonly IHolsterItemData _data;
        readonly HolsterScript _holster;

        public ShowInHolster(
            ItemScriptInstance item,
            IHolsterItemData data,
            HolsterScript holster)
        {
            _item = item;
            _data = data;
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Show();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Show();
        }

        void Show()
        {
            _holster.SetItem(_item, _data);
        }
    }
}