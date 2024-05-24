using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadInventoryData
    {
        public LoadInventoryData(
            IEnumerable<ILoadInventoryItem> items,
            IEnumerable<ILoadApparelPiece> apparelPieces)
        {
            Items = items.ToArray();
            ApparelPieces = apparelPieces.ToArray();
        }

        public IReadOnlyList<ILoadInventoryItem> Items { get; }

        public IReadOnlyList<ILoadApparelPiece> ApparelPieces { get; }
    }
}