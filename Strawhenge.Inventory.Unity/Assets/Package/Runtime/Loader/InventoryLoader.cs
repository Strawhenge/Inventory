using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using System;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class InventoryLoader
    {
        readonly ItemManager _itemManager;
        readonly ApparelManager _apparelManager;
        readonly IItemRepository _itemRepository;
        readonly IApparelRepository _apparelRepository;
        readonly ILogger _logger;

        public InventoryLoader(
            ItemManager itemManager,
            ApparelManager apparelManager,
            IItemRepository itemRepository,
            IApparelRepository apparelRepository,
            ILogger logger)
        {
            _itemManager = itemManager;
            _apparelManager = apparelManager;
            _itemRepository = itemRepository;
            _apparelRepository = apparelRepository;
            _logger = logger;
        }

        public void Load(InventoryLoadData data)
        {
            LoadHolsteredItems(data.HolsteredItems);
            LoadEquippedApparel(data.EquippedApparel);
        }

        void LoadHolsteredItems(HolsteredItemLoadDataEntry[] itemsData)
        {
            _logger.LogInformation("Loading holstered items.");

            foreach (var itemData in itemsData)
            {
                var itemResult = _itemRepository.FindByName(itemData.ItemName);

                if (!itemResult.HasSome(out var item))
                {
                    _logger.LogError($"Item '{itemData.ItemName}' cannot be found.");
                    continue;
                }

                _itemManager
                    .Manage(item)
                    .Holsters
                    .FirstOrNone(x => x.HolsterName == itemData.HolsterName)
                    .Do(x => x.Equip());
            }
        }

        void LoadEquippedApparel(string[] apparelNames)
        {
            foreach (var apparelName in apparelNames)
            {
                var apparelResult = _apparelRepository.FindByName(apparelName);

                if (!apparelResult.HasSome(out var apparel))
                {
                    _logger.LogError($"Apparel '{apparelName}' not found.");
                    continue;
                }

                _apparelManager
                    .Create(apparel)
                    .Equip();
            }
        }
    }
}