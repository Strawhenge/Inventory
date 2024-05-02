using Strawhenge.Inventory.Unity.Apparel;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<IApparelPieceData>> ApparelPieces { get; }
    }
}