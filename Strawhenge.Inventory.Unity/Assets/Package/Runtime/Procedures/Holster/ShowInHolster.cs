using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class ShowInHolster : Procedure
    {
        readonly ItemHelper _item;
        readonly HolsterScript _holster;

        public ShowInHolster(ItemHelper item, HolsterScript holster)
        {
            _item = item;
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
            _holster.SetItem(_item);
        }
    }
}
