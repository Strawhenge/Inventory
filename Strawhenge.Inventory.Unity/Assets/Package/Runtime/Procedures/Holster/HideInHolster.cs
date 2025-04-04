using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class HideInHolster : Procedure
    {
        readonly HolsterScript _holster;

        public HideInHolster(HolsterScript holster)
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
                .Do(x => x.Despawn());
        }
    }
}