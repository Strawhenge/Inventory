using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class ShowInHolster : Procedure
    {
        readonly IItemHelper _item;
        readonly IHolsterComponent _holster;

        public ShowInHolster(IItemHelper item, IHolsterComponent holster)
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
