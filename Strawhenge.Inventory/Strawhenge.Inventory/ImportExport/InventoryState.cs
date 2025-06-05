using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.ImportExport
{
    public class InventoryState
    {
        public static InventoryState Empty { get; } = new InventoryState();

        public InventoryState(IEnumerable<ItemState> items, IEnumerable<ApparelPieceState> apparelPieces)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (apparelPieces == null) throw new ArgumentNullException(nameof(apparelPieces));

            Items = items.ToArray();
            ApparelPieces = apparelPieces.ToArray();
        }

        InventoryState()
        {
            Items = Array.Empty<ItemState>();
            ApparelPieces = Array.Empty<ApparelPieceState>();
        }

        public IReadOnlyList<ItemState> Items { get; }

        public IReadOnlyList<ApparelPieceState> ApparelPieces { get; }
    }
}