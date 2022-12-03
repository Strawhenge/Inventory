using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class HideInHolster : Procedure
    {
        readonly IHolsterComponent _holster;

        public HideInHolster(IHolsterComponent holster)
        {
            _holster = holster;
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

        void Hide()
        {
            _holster.TakeItem()
                .Despawn();
        }
    }
}
