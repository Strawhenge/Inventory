using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class SimpleSpawnAndDrop : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IItemData _itemData;
        readonly Context _itemContext;
        readonly DropPoint _dropPoint;

        public SimpleSpawnAndDrop(
            ItemScriptInstance itemScriptInstance,
            IItemData itemData,
            Context itemContext,
            DropPoint dropPoint)
        {
            _itemScriptInstance = itemScriptInstance;
            _itemData = itemData;
            _itemContext = itemContext;
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
                var pickup = Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
                pickup.SetContext(_itemContext);
            });
        }
    }
}