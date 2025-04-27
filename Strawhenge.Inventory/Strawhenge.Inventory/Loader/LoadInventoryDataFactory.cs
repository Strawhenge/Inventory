using System.Collections.Generic;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Loader
{
    public class LoadInventoryDataFactory
    {
        readonly IItemRepository _itemRepository;
        readonly IApparelRepository _apparelRepository;
        readonly ILogger _logger;

        public LoadInventoryDataFactory(
            IItemRepository itemRepository,
            IApparelRepository apparelRepository,
            ILogger logger)
        {
            _itemRepository = itemRepository;
            _apparelRepository = apparelRepository;
            _logger = logger;
        }

        public LoadInventoryData Create(InventoryInfo info)
        {
            return new LoadInventoryData(GetLoadItems(info), GetLoadApparel(info));
        }

        IEnumerable<LoadInventoryItem> GetLoadItems(InventoryInfo info)
        {
            foreach (var itemInfo in info.Items)
            {
                if (!_itemRepository.FindByName(itemInfo.ItemName).HasSome(out var item))
                {
                    _logger.LogError($"Item '{itemInfo.ItemName}' not found.");
                    continue;
                }

                var dto = new LoadInventoryItem(item)
                {
                    InHand = itemInfo.IsInRightHand
                        ? LoadInventoryItemInHand.Right
                        : itemInfo.IsInLeftHand
                            ? LoadInventoryItemInHand.Left
                            : LoadInventoryItemInHand.None,
                    IsInStorage = itemInfo.IsInStorage
                };

                if (!string.IsNullOrWhiteSpace(itemInfo.HolsterName))
                    dto.SetHolster(itemInfo.HolsterName);

                yield return dto;
            }
        }

        IEnumerable<LoadApparelPiece> GetLoadApparel(InventoryInfo info)
        {
            foreach (var apparelPieceName in info.EquippedApparel)
            {
                if (!_apparelRepository.FindByName(apparelPieceName).HasSome(out var apparelPiece))
                {
                    _logger.LogError($"Apparel piece '{apparelPieceName}' not found.");
                    continue;
                }

                yield return new LoadApparelPiece(apparelPiece);
            }
        }
    }
}