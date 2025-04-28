using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHolster : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IItemData _itemData;
        readonly HolsterScript _holster;

        public SimpleDropFromHolster(
            ItemHelper itemHelper,
            IItemData itemData,
            HolsterScript holster)
        {
            _itemHelper = itemHelper;
            _itemData = itemData;
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
            _itemHelper.Despawn();
            _itemData.PickupPrefab.Do(pickupPrefab =>
            {
                var spawnPoint = _holster.GetItemDropPoint();
                Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
            });
        }
    }
}