using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IItemData _itemData;
        readonly DropPoint _dropPoint;

        public SimpleSpawnAndDrop(
            ItemHelper itemHelper,
            IItemData itemData,
            DropPoint dropPoint)
        {
            _itemHelper = itemHelper;
            _itemData = itemData;
            _dropPoint = dropPoint;
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
            _itemHelper.Despawn();
            _itemData.PickupPrefab.Do(pickupPrefab =>
            {
                var spawnPoint = _dropPoint.GetPoint();
                Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
            });
        }
    }
}
