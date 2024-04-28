using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class InventoryLoader
    {
        readonly IInventory _inventory;
        readonly IItemRepository _itemRepository;
        readonly IApparelRepository _apparelRepository;
        readonly ILogger _logger;

        public InventoryLoader(
            IInventory inventory,
            IItemRepository itemRepository,
            IApparelRepository apparelRepository,
            ILogger logger)
        {
            _inventory = inventory;
            _itemRepository = itemRepository;
            _apparelRepository = apparelRepository;
            _logger = logger;
        }

        public void Load(LoadInventoryData data)
        {
            LoadHolsteredItems(data.HolsteredItems);
            LoadEquippedApparel(data.EquippedApparel);
        }

        void LoadHolsteredItems(HolsteredItemLoadInventoryData[] itemsData)
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

                _inventory
                    .CreateItem(item)
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

                _inventory
                    .CreateApparelPiece(apparel)
                    .Equip();
            }
        }
    }
}