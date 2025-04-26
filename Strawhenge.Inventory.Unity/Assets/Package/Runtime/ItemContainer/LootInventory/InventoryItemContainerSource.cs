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

        public IReadOnlyList<IContainedItem<ItemData>> GetItems()
        {
            return _inventory
                .AllItems()
                .Select(item => new ContainedItem<ItemData>(item.Data, removeStrategy: item.Discard))
                .ToArray();
        }

        public IReadOnlyList<IContainedItem<ApparelPieceData>> GetApparelPieces()
        {
            return _inventory.ApparelSlots
                .Select(x => x.CurrentPiece)
                .WhereSome()
                .Select(apparelPiece =>
                    new ContainedItem<ApparelPieceData>(apparelPiece.Data, removeStrategy: apparelPiece.Discard))
                .ToArray();
        }
    }
}