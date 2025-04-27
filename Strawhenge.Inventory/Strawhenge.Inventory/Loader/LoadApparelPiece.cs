using Strawhenge.Inventory.Apparel;
using System;

namespace Strawhenge.Inventory.Loader
{
    public class LoadApparelPiece
    {
        public LoadApparelPiece(ApparelPieceData apparelPiece)
        {
            ApparelPiece = apparelPiece ?? throw new ArgumentNullException(nameof(apparelPiece));
        }

        public ApparelPieceData ApparelPiece { get; }
    }
}