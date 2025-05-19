using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;

namespace Strawhenge.Inventory.Unity
{
    public static class InventoryExtensions
    {
        public static InventoryItem CreateItem(
            this Inventory inventory,
            ItemPickupScript pickup)
        {
            var (item, context) = pickup.PickupItem();
            return inventory.CreateItem(item, context);
        }

        public static InventoryItem CreateItem(
            this Inventory inventory,
            ItemScriptableObject scriptableObject)
        {
            var item = scriptableObject.ToItem();
            return inventory.CreateItem(item);
        }

        public static InventoryApparelPiece CreateApparelPiece(
            this Inventory inventory,
            ApparelPieceScriptableObject scriptableObject)
        {
            var apparelPiece = scriptableObject.ToApparelPiece();
            return inventory.CreateApparelPiece(apparelPiece);
        }
    }
}