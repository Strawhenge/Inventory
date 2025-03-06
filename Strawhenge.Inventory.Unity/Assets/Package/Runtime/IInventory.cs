using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity
{
    public interface IInventory : Strawhenge.Inventory.IInventory
    {
        Item CreateItem(ItemPickupScript pickup);

        Item CreateItem(IItemData data);

        ApparelPiece CreateApparelPiece(IApparelPieceData data);
    }
}