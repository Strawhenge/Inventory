using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleSpawnAndDrop : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IItemData _itemData;
        readonly DropPoint _dropPoint;

        public SimpleSpawnAndDrop(
            ItemScriptInstance itemScriptInstance,
            IItemData itemData,
            DropPoint dropPoint)
        {
            _itemScriptInstance = itemScriptInstance;
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
            _itemScriptInstance.Despawn();
            _itemData.PickupPrefab.Do(pickupPrefab =>
            {
                var spawnPoint = _dropPoint.GetPoint();
                Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
            });
        }
    }
}
