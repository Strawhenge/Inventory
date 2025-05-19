using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items.ItemData;
using Strawhenge.Inventory.Unity.Items;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class SimpleDropFromHolster : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IItemData _itemData;
        readonly Context _itemContext;
        readonly HolsterScript _holster;

        public SimpleDropFromHolster(
            ItemScriptInstance itemScriptInstance,
            IItemData itemData,
            Context itemContext,
            HolsterScript holster)
        {
            _itemScriptInstance = itemScriptInstance;
            _itemData = itemData;
            _itemContext = itemContext;
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
            _itemScriptInstance.Despawn();
            _itemData.PickupPrefab.Do(pickupPrefab =>
            {
                var spawnPoint = _holster.GetItemDropPoint();
                var pickup = Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
                pickup.SetContext(_itemContext);
            });
        }
    }
}