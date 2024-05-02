using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
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
            return _inventory.LeftHand.CurrentItem.AsEnumerable()
                .Concat(_inventory.RightHand.CurrentItem.AsEnumerable())
                .Concat(_inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(_inventory.StoredItems)
                .Distinct()
                .SelectMany(item =>
                {
                    return _itemRepository
                        .FindByName(item.Name)
                        .Map(data => new ContainedItem<IItemData>(data, () =>
                        {
                            item.ClearFromHandsPreference = ClearFromHandsPreference.Disappear;
                            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Disappear;
                            item.ClearFromHands();
                            item.UnequipFromHolster();
                            _inventory.RemoveFromStorage(item);
                        }))
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