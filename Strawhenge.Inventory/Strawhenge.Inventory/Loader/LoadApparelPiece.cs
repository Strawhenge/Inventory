using Strawhenge.Inventory.Apparel;
using System;

namespace Strawhenge.Inventory.Loader
{
    public class LoadApparelPiece
    {
        public LoadApparelPiece(ApparelPiece apparelPiece)
        {
            ApparelPiece = apparelPiece ?? throw new ArgumentNullException(nameof(apparelPiece));
        }

        public ApparelPiece ApparelPiece { get; }
    }
}