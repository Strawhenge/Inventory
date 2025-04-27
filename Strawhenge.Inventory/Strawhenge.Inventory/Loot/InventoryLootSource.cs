using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Loot
{
    public class InventoryLootSource : ILootSource
    {
        readonly Inventory _inventory;

        public InventoryLootSource(Inventory inventory)
        {
            _inventory = inventory;
        }

        public IReadOnlyList<Loot<ItemData>> GetItems()
        {
            return _inventory
                .AllItems()
                .Select(item => new Loot<ItemData>(item.Data, onTake: item.Discard))
                .ToArray();
        }

        public IReadOnlyList<Loot<ApparelPieceData>> GetApparelPieces()
        {
            return _inventory.ApparelSlots
                .Select(x => x.CurrentPiece)
                .WhereSome()
                .Select(apparelPiece =>
                    new Loot<ApparelPieceData>(apparelPiece.Data, onTake: apparelPiece.Discard))
                .ToArray();
        }
    }
}