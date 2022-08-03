using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class ShowInHolster : Procedure
    {
        private readonly IItemHelper item;
        private readonly IHolsterComponent holster;

        public ShowInHolster(IItemHelper item, IHolsterComponent holster)
        {
            this.item = item;
            this.holster = holster;
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

        private void Show()
        {
            holster.SetItem(item);
        }
    }
}
