using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryItemContainerSource : IItemContainerSource
    {
        readonly Inventory _inventory;
        readonly IItemRepository _itemRepository;
        readonly IApparelRepository _apparelRepository;

        public InventoryItemContainerSource(
            Inventory inventory,
            IItemRepository itemRepository,
            IApparelRepository apparelRepository)
        {
            _inventory = inventory;
            _itemRepository = itemRepository;
            _apparelRepository = apparelRepository;
        }

        public IReadOnlyList<IContainedItem<ItemData>> GetItems()
        {
            return _inventory
                .AllItems()
                .SelectMany(item =>
                {
                    return _itemRepository
                        .FindByName(item.Name)
                        .Map(data => new ContainedItem<ItemData>(data, removeStrategy: item.Discard))
                        .AsEnumerable();
                })
                .ToArray();
        }

        public IReadOnlyList<IContainedItem<ApparelPieceData>> GetApparelPieces()
        {
            return _inventory.ApparelSlots
                .SelectMany(x => x.CurrentPiece.AsEnumerable())
                .SelectMany(apparelPiece =>
                {
                    return _apparelRepository
                        .FindByName(apparelPiece.Name)
                        .Map(data => new ContainedItem<ApparelPieceData>(data, apparelPiece.Discard))
                        .AsEnumerable();
                })
                .ToArray();
        }
    }
}