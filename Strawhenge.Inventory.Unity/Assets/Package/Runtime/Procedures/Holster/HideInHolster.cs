using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class HideInHolster : Procedure
    {
        private readonly IHolsterComponent holster;

        public HideInHolster(IHolsterComponent holster)
        {
            this.holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Hide();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Hide();
        }

        private void Hide()
        {
            holster.TakeItem()
                .Despawn();
        }
    }
}
