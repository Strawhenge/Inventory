using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Loader
{
    public class LoadInventoryData
    {
        public LoadInventoryData(
            IEnumerable<LoadInventoryItem> items,
            IEnumerable<LoadApparelPiece> apparelPieces)
        {
            Items = items.ToArray();
            ApparelPieces = apparelPieces.ToArray();
        }

        public IReadOnlyList<LoadInventoryItem> Items { get; }

        public IReadOnlyList<LoadApparelPiece> ApparelPieces { get; }
    }
}