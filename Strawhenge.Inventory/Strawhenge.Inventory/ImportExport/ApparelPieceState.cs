using System;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.ImportExport
{
    public class ApparelPieceState
    {
        public ApparelPieceState(ApparelPiece apparelPiece)
        {
            ApparelPiece = apparelPiece ?? throw new ArgumentNullException(nameof(apparelPiece));
        }

        public ApparelPiece ApparelPiece { get; }
    }
}