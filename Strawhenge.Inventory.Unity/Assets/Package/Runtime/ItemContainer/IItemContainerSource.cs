﻿using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<IItemData>> Items { get; }

        IReadOnlyList<IContainedItem<IApparelPieceData>> ApparelPieces { get; }
    }
}