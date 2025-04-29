using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;

namespace Strawhenge.Inventory.Unity
{
    public static class InventoryExtensions
    {
        public static Item CreateItem(
            this Inventory inventory,
            ItemPickupScript pickup)
        {
            var (data, context) = pickup.PickupItem();
            return inventory.CreateItem(data, context);
        }

        public static Item CreateItem(
            this Inventory inventory,
            ItemScriptableObject scriptableObject)
        {
            var data = scriptableObject.ToItemData();
            return inventory.CreateItem(data);
        }

        public static ApparelPiece CreateApparelPiece(
            this Inventory inventory,
            ApparelPieceScriptableObject scriptableObject)
        {
            var data = scriptableObject.ToApparelPieceData();
            return inventory.CreateApparelPiece(data);
        }
    }
}