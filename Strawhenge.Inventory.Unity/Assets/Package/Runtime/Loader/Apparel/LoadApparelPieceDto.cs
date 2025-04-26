using Strawhenge.Inventory.Apparel;
using System;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadApparelPieceDto : ILoadApparelPiece
    {
        public LoadApparelPieceDto(ApparelPieceData apparelPiece)
        {
            ApparelPiece = apparelPiece ?? throw new ArgumentNullException(nameof(apparelPiece));
        }

        public ApparelPieceData ApparelPiece { get; }
    }
}