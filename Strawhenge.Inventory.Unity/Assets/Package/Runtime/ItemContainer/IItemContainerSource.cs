using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<ItemData>> GetItems();

        IReadOnlyList<IContainedItem<IApparelPieceData>> GetApparelPieces();
    }
}