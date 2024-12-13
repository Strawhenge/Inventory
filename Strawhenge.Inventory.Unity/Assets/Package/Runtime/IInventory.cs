using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity
{
    public interface IInventory : Strawhenge.Inventory.IInventory
    {
        IItem CreateItem(ItemPickupScript pickup);

        IItem CreateItem(IItemData data);

        IApparelPiece CreateApparelPiece(IApparelPieceData data);
    }
}