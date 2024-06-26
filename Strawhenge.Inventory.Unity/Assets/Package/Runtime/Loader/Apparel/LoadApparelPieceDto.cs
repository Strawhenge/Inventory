﻿using Strawhenge.Inventory.Unity.Apparel;
using System;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadApparelPieceDto : ILoadApparelPiece
    {
        public LoadApparelPieceDto(IApparelPieceData apparelPiece)
        {
            ApparelPiece = apparelPiece ?? throw new ArgumentNullException(nameof(apparelPiece));
        }

        public IApparelPieceData ApparelPiece { get; }
    }
}