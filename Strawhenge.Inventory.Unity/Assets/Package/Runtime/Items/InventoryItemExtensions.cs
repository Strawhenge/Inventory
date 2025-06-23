using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public static class InventoryItemExtensions
    {
        public static Maybe<ItemPickupScript> Take(
            this InventoryItem inventoryItem,
            Vector3 position,
            Quaternion rotation)
        {
            inventoryItem.Discard();

            return inventoryItem.Item
                .Get<IItemData>()
                .Map(data => data.PickupPrefab)
                .Flatten()
                .Map(prefab =>
                {
                    var pickup = Object.Instantiate(prefab, position, rotation);
                    pickup.SetContext(inventoryItem.Context);
                    return pickup;
                });
        }
    }
}