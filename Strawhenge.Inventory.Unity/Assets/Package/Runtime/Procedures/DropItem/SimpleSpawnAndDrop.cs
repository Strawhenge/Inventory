using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        readonly ItemHelper _item;

        public SimpleSpawnAndDrop(ItemHelper item)
        {
            _item = item;
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
            _item.Release();
        }
    }
}
