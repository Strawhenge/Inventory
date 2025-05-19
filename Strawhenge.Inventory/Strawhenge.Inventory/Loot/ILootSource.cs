using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Loot
{
    public interface ILootSource
    {
        IReadOnlyList<Loot<Item>> GetItems();

        IReadOnlyList<Loot<ApparelPieceData>> GetApparelPieces();
    }
}