using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<ItemData>> GetItems();

        IReadOnlyList<IContainedItem<ApparelPieceData>> GetApparelPieces();
    }
}