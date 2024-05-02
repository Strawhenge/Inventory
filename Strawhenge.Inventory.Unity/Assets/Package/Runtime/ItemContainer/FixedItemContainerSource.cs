using Strawhenge.Inventory.Unity.Apparel;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class FixedItemContainerSource : IItemContainerSource
    {
        readonly List<IApparelPieceData> _apparelPieces;

        public FixedItemContainerSource(IEnumerable<IApparelPieceData> apparelPieces)
        {
            _apparelPieces = apparelPieces.ToList();
        }

        public IReadOnlyList<IContainedItem<IApparelPieceData>> ApparelPieces => _apparelPieces
            .Select(apparelPiece =>
                new ContainedItem<IApparelPieceData>(
                    apparelPiece,
                    removeStrategy: () => _apparelPieces.Remove(apparelPiece)))
            .ToArray();
    }
}