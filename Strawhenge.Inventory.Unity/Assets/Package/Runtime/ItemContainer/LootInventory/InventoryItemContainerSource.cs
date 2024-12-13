using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryItemContainerSource : IItemContainerSource
    {
        readonly IInventory _inventory;
        readonly IItemRepository _itemRepository;
        readonly IApparelRepository _apparelRepository;

        public InventoryItemContainerSource(
            IInventory inventory,
            IItemRepository itemRepository,
            IApparelRepository apparelRepository)
        {
            _inventory = inventory;
            _itemRepository = itemRepository;
            _apparelRepository = apparelRepository;
        }

        public IReadOnlyList<IContainedItem<IItemData>> GetItems()
        {
            return _inventory
                .AllItems()
                .SelectMany(item =>
                {
                    return _itemRepository
                        .FindByName(item.Name)
                        .Map(data => new ContainedItem<IItemData>(data, removeStrategy: item.Discard))
                        .AsEnumerable();
                })
                .ToArray();
        }

        public IReadOnlyList<IContainedItem<IApparelPieceData>> GetApparelPieces()
        {
            return _inventory.ApparelSlots
                .SelectMany(x => x.CurrentPiece.AsEnumerable())
                .SelectMany(apparelPiece =>
                {
                    return _apparelRepository
                        .FindByName(apparelPiece.Name)
                        .Map(data => new ContainedItem<IApparelPieceData>(data, apparelPiece.Unequip))
                        .AsEnumerable();
                })
                .ToArray();
        }
    }
}