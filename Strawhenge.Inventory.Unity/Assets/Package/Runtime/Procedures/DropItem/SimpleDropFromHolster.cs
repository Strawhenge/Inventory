using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHolster : Procedure
    {
        readonly HolsterScript _holster;

        public SimpleDropFromHolster(HolsterScript holster)
        {
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Drop();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Drop();
        }

        void Drop()
        {
            _holster.TakeItem().Do(
                x => x.Release());
        }
    }
}