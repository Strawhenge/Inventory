using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Procedures.DropItem
{
    public class SimpleDropFromHand : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IItemData _itemData;
        readonly Context _itemContext;
        readonly HandScript _hand;

        public SimpleDropFromHand(
            ItemScriptInstance itemScriptInstance,
            IItemData itemData,
            Context itemContext,
            HandScript hand)
        {
            _itemScriptInstance = itemScriptInstance;
            _itemData = itemData;
            _itemContext = itemContext;
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
            _itemScriptInstance.Despawn();
            _itemData.PickupPrefab.Do(pickupPrefab =>
            {
                var spawnPoint = _hand.GetItemDropPoint();
                var pickup = Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
                pickup.SetContext(_itemContext);
            });
        }
    }
}