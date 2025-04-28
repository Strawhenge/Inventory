using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHand : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly IItemData _itemData;
        readonly HandScript _hand;

        public SimpleDropFromHand(
            ItemHelper itemHelper,
            IItemData itemData,
            HandScript hand)
        {
            _itemHelper = itemHelper;
            _itemData = itemData;
            _hand = hand;
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
                var spawnPoint = _hand.GetItemDropPoint();
                Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
            });
        }
    }
}