using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;

namespace Strawhenge.Inventory.Unity
{
    public interface IInventory : Strawhenge.Inventory.IInventory
    {
        IItem CreateItem(ItemScript script);

        IItem CreateItem(IItemData data);

        IApparelPiece CreateApparelPiece(IApparelPieceData data);
    }
}