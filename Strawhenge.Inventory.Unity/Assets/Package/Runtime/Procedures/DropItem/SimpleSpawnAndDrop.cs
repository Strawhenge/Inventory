using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        private readonly IItemHelper item;
        private readonly IItemDropPoint itemDropPoint;

        public SimpleSpawnAndDrop(IItemHelper item, IItemDropPoint itemDropPoint)
        {
            this.item = item;
            this.itemDropPoint = itemDropPoint;
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

        private void Drop()
        {
            var point = itemDropPoint.GetPoint();

            var itemScript = item.Spawn();
            itemScript.transform.SetPositionAndRotation(point, Quaternion.Euler(Vector3.forward));
            item.Release();
        }
    }
}
