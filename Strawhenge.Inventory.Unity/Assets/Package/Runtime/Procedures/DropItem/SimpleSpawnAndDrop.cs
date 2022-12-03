using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        readonly IItemHelper _item;
        readonly IItemDropPoint _itemDropPoint;

        public SimpleSpawnAndDrop(IItemHelper item, IItemDropPoint itemDropPoint)
        {
            _item = item;
            _itemDropPoint = itemDropPoint;
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
            var point = _itemDropPoint.GetPoint();

            var itemScript = _item.Spawn();
            itemScript.transform.SetPositionAndRotation(point, Quaternion.Euler(Vector3.forward));
            _item.Release();
        }
    }
}
