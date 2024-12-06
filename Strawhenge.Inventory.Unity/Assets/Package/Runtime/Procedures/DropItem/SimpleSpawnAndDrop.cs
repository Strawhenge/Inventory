using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        readonly IItemHelper _item;

        public SimpleSpawnAndDrop(IItemHelper item)
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
