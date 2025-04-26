using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryItemContainerSource : IItemContainerSource
    {
        readonly Inventory _inventory;

        public InventoryItemContainerSource(Inventory inventory)
        {
            _inventory = inventory;
        }

        public IReadOnlyList<ContainedItem<ItemData>> GetItems()
        {
            return _inventory
                .AllItems()
                .Select(item => new ContainedItem<ItemData>(item.Data, onRemove: item.Discard))
                .ToArray();
        }

        public IReadOnlyList<ContainedItem<ApparelPieceData>> GetApparelPieces()
        {
            return _inventory.ApparelSlots
                .Select(x => x.CurrentPiece)
                .WhereSome()
                .Select(apparelPiece =>
                    new ContainedItem<ApparelPieceData>(apparelPiece.Data, onRemove: apparelPiece.Discard))
                .ToArray();
        }
    }
}